using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("users")]
    internal partial class User : IUser
    {
        [Key]
        public int Id { get; set; }

        private string surname = string.Empty;

        [MaxLength(100)]
        public string Surname { get => surname; set => surname = value ?? string.Empty; }

        private string name = string.Empty;

        [MaxLength(100)]
        public string Name { get => name; set => name = value ?? string.Empty; }

        [MaxLength(100)]
        public string? Patronymic { get; internal set; }


        private string login = string.Empty;
        public string Login { get => login; set => login = value ?? string.Empty; }

        /// <summary>Хеш пароля.</summary>
        public byte[]? HashPassword { get; set; }

        /// <summary>Строковый пароль. Временное решение.</summary>
        public string? Password { get; set; }

        public int RoleId { get; set; } //не убирайте это св-во. на него есть ссылка в OnModelCreating

        public virtual Role Role { get; set; } = null!;

        IRole? IUser.Role => Role;
    }
}
