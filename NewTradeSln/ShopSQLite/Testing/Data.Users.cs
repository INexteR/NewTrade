using Model;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static User[]? users;
        public static IEnumerable<IUser> GetUsers()
        {
            if (users is null)
            {
                var lines = File.ReadAllLines(usersDataFullName);
                users = new User[lines.Length];

                if (roles is null) GetRoles();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    int roleId = int.Parse(props[6]);
                    users[i] = new User
                    {
                        Id = int.Parse(props[0]),
                        Surname = props[1],
                        Name = props[2],
                        Patronymic = props[3],
                        Login = props[4],
                        Password = props[5],
                        HashPassword = ModelHelper.GetHashPassword(props[5]),
                        RoleId = roleId,
                        Role = roles![roleId -1]
                    };
                }
            }

            return users;
        }
    }
}
