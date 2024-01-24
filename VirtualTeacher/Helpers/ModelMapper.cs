using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;

namespace VirtualTeacher.Helpers
{
    public class ModelMapper
    {
        //User DTOs
        public User MapCreate(UserCreateDto dto)
        {
            return new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                AvatarUrl = dto.AvatarUrl,
                UserRole = dto.UserRole
            };
        }

        public UserResponseDto MapResponse(User user)
        {
            return new UserResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserRole = user.UserRole.ToString()
            };
        }

        public User MapUpdate(UserUpdateDto dto)
        {
            return new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                AvatarUrl = dto.AvatarUrl
            };
        }
    }
}
