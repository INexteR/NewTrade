using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    public static partial class TestData
    {
        private static User[]? users;
        public static IEnumerable<User> GetUsers()
        {
            // Ициализация users, если не был инициализирован.
            if (users is null)
            {
                var lines = File.ReadAllLines(usersDataFullName);
                users = new User[lines.Length];

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
                        Email = props[4],
                        Login = props[5],
                        Password = props[6],
                        HashPassword = ModelHelper.GetHashPassword(props[6]),
                    };
                }
            }

            // Возврат копий, чтобы имитировать запрос к БД.
            // В каждом запросе возвращаются разные сущности, но с одинаковыми значениями.
            foreach (var user in users)
            {
                yield return user.Clone();
            }
        }
    }
}
