using Interfaces;

namespace ShopModel.Entities
{
    public partial class User : IUser
    {

        public int Id { get; internal set; }

        private string surname = string.Empty;

        public string Surname { get => surname; internal set => surname = value ?? string.Empty; }

        private string name = string.Empty;

        public string Name { get => name; internal set => name = value ?? string.Empty; }
        public string? Patronymic { get; internal set; }


        private string login = string.Empty;
        public string Login { get => login; internal set => login = value ?? string.Empty; }

        /// <summary>Хеш пароля.</summary>
        internal byte[]? HashPassword { get; set; }

        internal virtual Role? Role { get; set; }

        IRole? IUser.Role => Role;
    }
}
