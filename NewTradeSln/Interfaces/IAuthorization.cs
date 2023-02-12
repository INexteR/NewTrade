
namespace Interfaces
{
    /// <summary>
    /// Служба авторизации.
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// Текущее состояние авторизации
        /// </summary>
        AuthorizationStatus AuthorizationStatus { get; }
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        IUser? CurrentUser { get; }
        /// <summary>
        /// Метод, выполняющий вход. При нахождении пользователя с указанными данными устанавливает свойство CurrentUser
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        void Login(string login, string password);
        /// <summary>
        /// Асинхронный двойник метода Login
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        Task LoginAsync(string login, string password);
        /// <summary>
        /// Метод, выполняющий завершение сеанса текущего пользователя. Обнуляет свойство CurrentUser
        /// </summary>
        void Exit();
    }
}
