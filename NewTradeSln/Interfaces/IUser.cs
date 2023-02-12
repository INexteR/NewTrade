
namespace Interfaces
{
    /// <summary>Пользователь/</summary>
    public interface IUser
    {
        /// <summary>Идентификатор.</summary>
        int Id { get; }

        /// <summary>Фамилия.</summary>
        string Surname { get; }

        /// <summary>Имя.</summary>
        string Name { get; }

        /// <summary>Отчество.</summary>
        string Patronymic { get; }

        /// <summary>Логин.</summary>
        string Login { get; }

        /// <summary>Хеш пароля.</summary>
        byte[] HashPassword { get; }

        /// <summary>Роль.</summary>
        IRole UserRole { get; }
    }
}
