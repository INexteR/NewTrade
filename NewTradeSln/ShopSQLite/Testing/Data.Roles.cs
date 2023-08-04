using Mapping;
using Model;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        public static Role[] roles =
        {
            new Role { Id = 1, Name = "Администратор"},
            new Role { Id = 2, Name = "Менеджер"},
            new Role { Id = 3, Name = "Клиент"}
        };

    }
}
