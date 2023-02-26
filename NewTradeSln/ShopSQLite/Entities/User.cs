using Model;

namespace ShopSQLite.Entities
{
    internal partial class User : IUser
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
        public byte[]? HashPassword { get; set; }

        /// <summary>Строковый пароль. Временное решение.</summary>
        private string password = string.Empty;
        public string Password { get => password; internal set => password = value ?? string.Empty; }

        public int RoleId { get; set; } //не убирайте это св-во. на него есть ссылка в OnModelCreating

        public virtual Role Role { get; set; } = null!;

        IRole? IUser.Role => Role;
    }
}
