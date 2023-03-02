using Mapping;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Role[]? roles;
        public static IEnumerable<Role> GetRoles()
        {
            return roles ??= rolesText.LinesToArray<Role>(nameof(Role.Id), 
                                                          nameof(Role.Name), 
                                                          nameof(Role.Rights));
        }

        private const string rolesText = @"
1	Администратор   Full
2	Менеджер    Viewing
3	Клиент  Viewing
4	Гость   Viewing";
    }
}
