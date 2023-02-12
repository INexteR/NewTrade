
namespace Interfaces
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        string Patronymic { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        IRole UserRole { get; set; }
    }
}
