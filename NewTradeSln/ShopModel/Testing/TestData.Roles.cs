using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    public static partial class TestData
    {
        private static readonly string rolesDataFullName = Path.Combine(folderFullName, testUsersData);
        private static Role[]? roles;
        public static IEnumerable<Role> GetRoles()
        {
            // Ициализация users, если не был инициализирован.
            if (roles is null)
            {
                var lines = File.ReadAllLines(rolesDataFullName);
                roles = new Role[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    roles[i] = new Role
                    {
                        Id = int.Parse(props[0]),
                        Name = props[2]
                    };
                }
            }

            // Возврат копий, чтобы имитировать запрос к БД.
            // В каждом запросе возвращаются разные сущности, но с одинаковыми значениями.
            foreach (var role in roles)
            {
                yield return role.Clone();
            }
        }
    }
}
