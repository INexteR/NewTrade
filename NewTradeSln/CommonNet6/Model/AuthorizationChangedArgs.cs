using System;

namespace Model
{
    /// <summary>Аргумент события изменения состояния авторизации.</summary>
    public class AuthorizationChangedArgs : EventArgs
    {
        /// <summary>Новое состояние авторизации.</summary>
        public AuthorizationStatus NewStatus { get; }

        /// <summary>Авторизированный пользователь. Допустимо только для 
        /// <see cref="NewStatus"></see>=<see cref="AuthorizationStatus.Authorized">Authorized</see>.</summary>
        public IUser? NewUser { get; }

        /// <summary>Создаёт экземпляр <see cref="AuthorizationChangedArgs"/>.</summary>
        /// <param name="newStatus">Новое состояние авторизации.</param>
        /// <param name="newUser">Авторизированный пользователь. Допустимо только для 
        /// <see cref="NewStatus"></see>=<see cref="AuthorizationStatus.Authorized">Authorized</see>.</param>
        /// <exception cref="ArgumentException">Если <paramref name="newStatus"/> не одно из значений:
        /// <see cref="AuthorizationStatus.Authorized">Authorized</see>,
        /// <see cref="AuthorizationStatus.InProcessing">InProcessing</see>,
        /// <see cref="AuthorizationStatus.Authorized">Authorized</see>.<br/>
        /// Или если <paramref name="newUser"/>!=<see langword="null"/> и
        /// <paramref name="newStatus"/>!=<see cref="AuthorizationStatus.Authorized">Authorized</see>.</exception>
        /// <exception cref="ArgumentNullException">Если
        /// <paramref name="newStatus"/>=<see cref="AuthorizationStatus.Authorized">Authorized</see>,
        /// а <paramref name="newUser"/>==<see langword="null"/>.</exception>
        public AuthorizationChangedArgs(AuthorizationStatus newStatus, IUser? newUser = null)
        {
            if (!Enum.IsDefined(newStatus))
            {
                throw new ArgumentException("Неожиданное значение.", nameof(newStatus));
            }

            if (newStatus is AuthorizationStatus.InProcessing or AuthorizationStatus.None 
                && newUser is not null)
            {
                throw new ArgumentException($"Допустимо только для {AuthorizationStatus.Authorized}.", 
                    nameof(newUser));
            }

            NewStatus = newStatus;
            NewUser = newUser;
        }
    }
}
