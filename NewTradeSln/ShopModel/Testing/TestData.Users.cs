using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    internal static partial class TestData
    {
        private static User[]? users;
        public static IEnumerable<IUser> GetUsers()
        {
            if (users is null)
            {
                var lines = File.ReadAllLines(usersDataFullName);
                users = new User[lines.Length];

                if (roles is null) foreach(var _ in GetRoles()) { }

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    users[i] = new User
                    {
                        Id = int.Parse(props[0]),
                        Surname = props[1],
                        Name = props[2],
                        Patronymic = props[3],
                        Login = props[4],
                        Password = props[5],
                        HashPassword = ModelHelper.GetHashPassword(props[5]),
                        Role = roles![int.Parse(props[6]) - 1]
                    };
                }
            }

            foreach (var user in users)
            {
                yield return user.Clone();
            }
        }
    }
}
