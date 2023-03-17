
namespace Model
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
        string? Patronymic { get; }

        /// <summary>Логин.</summary>
        string Login { get; }

        /// <summary>Пароль.</summary>
        string? Password { get; }

        /// <summary>Роль.</summary>
        IRole Role { get; }
    }
}
