using Interfaces;

namespace ShopModel.Entities
{
    /// <summary>Вспомогательные служба.</summary>
    internal static partial class ModelHelper
    {

        /// <summary>Проверка стрингового пароля.</summary>
        /// <param name="password">Пароль.</param>
        /// <returns><see langword="true"/>, если хеш <paramref name="password"/>
        /// совпадает с <see cref="HashPassword"/>.</returns>
        public static bool CheckPassword(this IUser iuser, string? password)
        {
            User user = (User)iuser;
            // Проверка логина без пароля (вход без пароля).
            if (password is null || user.HashPassword is null)
                return password is null && user.HashPassword is null;

            return user.HashPassword.SequenceEqual(GetHashPassword(password) ?? Array.Empty<byte>());
        }

        public static byte[]? GetHashPassword(string? password)
        {
            if (password is null)
                return null;

            // Получение хеша пароля. В данном случае просто для примера используется дефолтный хеш стринга.
            return BitConverter.GetBytes(password.GetHashCode());
        }
    }
}
