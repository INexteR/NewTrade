
namespace Interfaces
{
    /// <summary>Служба авторизации.</summary>
    public interface IAuthorization
    {
        /// <summary>Текущее состояние авторизации.</summary>
        AuthorizationStatus Status { get; }

        /// <summary>Текущий пользователь.</summary>
        /// <remarks>Может быть отлично от <see langword="null"/>
        /// только после успешной авторизации.</remarks>
        IUser? CurrentUser { get; }

        /// <summary>Метод, выполняющий вход. При нахождении пользователя с указанными данными устанавливает свойство CurrentUser.</summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <remarks>Запускает процесс авторицации. На время обработки устанавливается
        /// <see cref="Status">свойство AuthorizationStatus</see>=<see cref="AuthorizationStatus.InProcessing"/>.<br/>
        /// Вызов метода разрещён только для состояния <see cref="Status"/>=<see cref="AuthorizationStatus.None"/>.<br/>
        /// После обработки авторизаци:<br/>
        /// - если успешна, то <see cref="Status">AuthorizationStatus</see>=<see cref="AuthorizationStatus.Authorized">Authorized</see>
        /// и устанавливается значение <see cref="CurrentUser">свойству CurrentUser</see>;<br/>
        /// - если ошибка или неуспешна, то <see cref="Status">AuthorizationStatus</see>=<see cref="AuthorizationStatus.None">None</see>,
        /// <see cref="CurrentUser">CurrentUser</see>=<see langword="null"/>.<br/><br/>
        /// Метод <see cref="Login">Login</see> не ждёт завершения процесса авторизации и завершается сразу после запуска процесса.</remarks>
        void Login(string login, string password);

        /// <summary>Асинхронный двойник метода Login.</summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns><see cref="Task"/> с методом <see cref="Login"/>,
        /// запущенным на выполнение в пуле потоков.</returns>
        void LoginAsync(string login, string password);

        /// <summary>Метод, выполняющий завершение сеанса текущего пользователя.
        /// Обнуляет <see cref="CurrentUser">свойство CurrentUser</see>
        /// и устанавливает <see cref="Status">свойство AuthorizationStatus</see>=<see cref="AuthorizationStatus.None"/>.</summary>
        void Exit();
    }
}
