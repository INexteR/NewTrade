using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    internal static partial class TestData
    {
        private static Role[]? roles;
        public static IEnumerable<IRole> GetRoles()
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
                        Name = props[1]
                    };
                }
            }

            foreach (var role in roles)
            {
                yield return role.Clone();
            }
        }
    }
}
