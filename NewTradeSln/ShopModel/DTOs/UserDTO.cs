
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
            Id = user.Id;
            Surname = user.Surname;
            Name = user.Name;
            Patronymic = user.Patronymic??string.Empty;
            Login = user.Login;
            Password = user.Password??string.Empty;
            Role = new(user.Role);
        }
    }
}
