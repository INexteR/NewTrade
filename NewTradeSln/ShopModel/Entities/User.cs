using Interfaces;

namespace ShopModel.Entities
{
    public partial class User : IUser
    {

        public int Id { get; set; }

        private string surname = string.Empty;

        public string Surname { get => surname; set => surname = value ?? string.Empty; }

        private string name = string.Empty;

        public string Name { get => name; set => name = value ?? string.Empty; }
        public string? Patronymic { get; set; }


        private string login = string.Empty;
        public string Login { get => login; set => login = value ?? string.Empty; }

        public byte[]? HashPassword { get; set; }

        internal virtual Role? Role { get; set; }
        IRole? IUser.Role => Role;

        public bool CheckPassword(string password)
        {
            // Значит для логина нет пароля (вход без пароля).
            if (password is null || HashPassword is null)
                return password is null && HashPassword is null;

            // Получение хеша пароля. В данном случае просто для примера используется дефолтный хеш стринга.
            byte[] hash = BitConverter.GetBytes(password.GetHashCode());

            return hash.SequenceEqual(HashPassword);
        }
    }
}
