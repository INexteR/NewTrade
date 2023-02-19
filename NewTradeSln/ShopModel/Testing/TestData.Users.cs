using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    public static partial class TestData
    {
        private const string testUsersData = @"users.txt";
        private static readonly string usersDataFullName = Path.Combine(folderFullName, testUsersData);

        public static IEnumerable<User> GetUsers()
        {
            var roles = GetRoles();
            var lines = File.ReadAllLines(usersDataFullName);
            foreach (var line in lines)
            {
                string[] props = line.Split('\t');
                yield return new User
                {
                    Id = int.Parse(props[0]),
                    Surname = props[1],
                    Name = props[2],
                    Patronymic = props[3],
                    Login = props[4],
                    Password = props[5],
                    HashPassword = ModelHelper.GetHashPassword(props[5]),
                    Role = roles[int.Parse(props[6]) - 1]
                };
            }
        }
    }
}
