using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository userRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IConfiguration config;
    private readonly IWebHostEnvironment environment;

    public AccountService(IUserRepository userRepository, IConfiguration config,
    IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
    {
        this.userRepository = userRepository;
        this.config = config;
        this.httpContextAccessor = httpContextAccessor;
        this.environment = environment;
    }

    public string GenerateToken(CredentialsDto credentialsDto)
    {
        var user = userRepository.GetByEmail(credentialsDto.Email);

        if (user == null)
        {
            throw new EntityNotFoundException("Invalid credentials.");
        }

        List<Claim> claims = new List<Claim>
        {
            new("UserId", user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.UserRole.ToString())
        };

        switch (user.UserRole)
        {
            case UserRole.Admin:
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                break;
            case UserRole.Teacher:
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                break;
            case UserRole.Student:
                claims.Add(new Claim(ClaimTypes.Role, "Student"));
                break;
            default:
                claims.Add(new Claim(ClaimTypes.Role, "Anonymous"));
                break;
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            config.GetSection("Jwt:Key").Value!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

    public UserRole CheckUserRole(User user)
    {
        switch (user.UserRole)
        {
            case UserRole.Admin:
                return UserRole.Admin;
            case UserRole.Teacher:
                return UserRole.Teacher;
            case UserRole.Student:
                return UserRole.Student;
            default:
                return UserRole.Anonymous;
        }
    }

    public string EncodePassword(string password)
    {
        var encodedPassword = Encoding.UTF8.GetBytes(password);

        return Convert.ToBase64String(encodedPassword);
    }

    public int GetLoggedUserId()
    {
        var result = -1;

        if (httpContextAccessor.HttpContext is null)
        {
            throw new UnauthorizedOperationException("You are not logged in!");
        }

        if (httpContextAccessor.HttpContext.User.FindFirstValue("UserId") is not null
            && httpContextAccessor.HttpContext.User.FindFirstValue("UserId") != null)
        {
            result = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
        }

        return result;
    }

    public User GetLoggedUser()
    {
        var loggedId = GetLoggedUserId();

        if (loggedId == -1)
        {
            throw new UnauthorizedOperationException("You are not logged in!");
        }

        var loggedUser = userRepository.GetById(loggedId);

        return loggedUser ?? throw new UnauthorizedOperationException("You are not logged in!");
    }

    public bool UserIsLoggedIn()
    {
        var isLoggedIn = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        return isLoggedIn;
    }

    public void ValidateAdminRole()
    {
        var currentRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        if (currentRole != UserRole.Admin.ToString())
        {
            throw new UnauthorizedOperationException("You are not an admin!");
        }
    }

    public void ValidateAuthorOrAdmin(int userId)
    {
        var currentRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        var currentId = GetLoggedUserId();

        if (currentRole != UserRole.Admin.ToString() && currentId != userId)
        {
            throw new UnauthorizedOperationException("You are not an author or admin!");
        }
    }

    public bool ValidateCredentials(string email, string password)
    {
        User? user = userRepository.GetByEmail(email);

        if (user == null || user.Password != Sha512(password))
        {
            return false;
        }

        return true;
    }

    public User AccountUpdate(AccountUpdateDto dto)
    {
        var user = GetLoggedUser();

        if (dto.Email != null
            && userRepository.CheckDuplicateEmail(dto.Email)
            && user.Email != dto.Email)
        {
            throw new DuplicateEntityException($"Email {dto.Email} is already in use!");
        }

        dto.Password = dto.Password != null ? Sha512(dto.Password) : user.Password;

        var updatedUser = userRepository.UpdateAccount(user.Id, dto);

        return updatedUser ?? throw new Exception("Your profile could not be updated.");
    }

    public string Sha512(string input)
    {
        using var sha = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }

    public string SaveAccountAvatar(IFormFile file)
    {
        if (Path.GetExtension(file.FileName).ToLowerInvariant() != ".jpg")
        {
            throw new ArgumentException("Only *.jpg files are allowed.");
        }

        var user = GetLoggedUser();

        var uploadsFolderPath = Path.Combine(environment.WebRootPath, "images/avatars");

        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        var filePath = Path.Combine(uploadsFolderPath, user.Username + ".jpg");
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }

        return Path.Combine("avatars", user.Username + ".jpg");
    }

    public string GetUserAvatar(string username)
    {
        const string avatarFolderPath = "images/avatars";

        var avatarFilePath = Path.Combine(environment.WebRootPath, avatarFolderPath, $"{username}.jpg");

        if (File.Exists(avatarFilePath))
        {
            return $"{avatarFolderPath}/{username}.jpg";
        }

        return "images/avatar-default.jpg";
    }

    public bool DeleteUserAvatar(string username)
    {
        const string avatarFolderPath = "images/avatars";

        var filePath = Path.Combine(environment.WebRootPath, avatarFolderPath, $"{username}.jpg");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        return false;
    }

    public List<Course> GetCompletedCourses()
    {
        var user = GetLoggedUser();

        return user.EnrolledCourses
            .Where(course => course.Lectures.
                All(lecture => lecture.Submissions
                    .Any(s => s.StudentId == user.Id)))
            .ToList();
    }

    public List<Course> GetRatedCourses()
    {
        var user = GetLoggedUser();

        return user.Ratings
            .Select(rating => rating.Course)
            .ToList();
    }
}