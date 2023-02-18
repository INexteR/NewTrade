
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class RoleDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public RoleDTO(Role role)
        {
            Id = role.RoleId;
            Name = role.RoleName;
        }
    }
}
