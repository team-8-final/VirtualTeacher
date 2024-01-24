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
    }
}
