using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    public static partial class TestData
    {
        public static Role[] GetRoles()
        {
            return new Role[]
            {
                new Role{ Id = 1, Name = "Администратор" },
                new Role{ Id = 2, Name = "Менеджер" },
                new Role{ Id = 3, Name = "Клиент" }
                //роль гостя в базе вообще не хранится
            };
        }
    }
}
