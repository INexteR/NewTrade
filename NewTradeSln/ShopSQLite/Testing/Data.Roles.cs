using Model;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Role[]? roles;
        public static Role[] GetRoles()
        {
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
                        Name = props[1],
                        Rights = Enum.Parse<Rights>(props[2])
                    };
                }
            }

            return roles;

            //foreach (var role in roles)
            //{
            //    yield return role.Clone();
            //}
        }
    }
}
