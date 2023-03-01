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

        [MaxLength(100)]
        public string Surname { get; set; } = null!;

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Patronymic { get; set; }

        public string Login { get; set; } = null!;

        /// <summary>Хеш пароля.</summary>
        public byte[]? HashPassword { get; set; }

        /// <summary>Строковый пароль. Временное решение.</summary>
        public string? Password { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;

        IRole? IUser.Role => Role;
    }
}
