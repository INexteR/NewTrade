
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class UserDTO
    {
        public int Id { get; }
        public string Surname { get; set; }
        public string Name { get; set; } 
        public string Patronymic { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public RoleDTO Role { get; set; }

        public UserDTO(User user)
        {
            Id = user.UserId;
            Surname = user.UserSurname;
            Name = user.UserName;
            Patronymic = user.UserPatronymic;
            Login = user.UserLogin;
            Password = user.UserPassword;
            Role = new(user.UserRoleNavigation);
        }
    }
}
