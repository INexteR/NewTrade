using ShopModel.Entities;

namespace ShopModel
{
    public class Authorization
    {
        public async Task Login(string login, string password)
        {
            await using var context = await Context.GetAsync();
            await foreach (var user in context.Users)
            {
                if (Eq(user.UserLogin, login) && Eq(user.UserPassword, password))
                {
                    CurrentUser = user;
                    StatusMessage = "Пользователь авторизован";
                    return;
                }
            }
            StatusMessage = "Пользователь не найден";
        }

        private static bool Eq(string s1, string s2)
        {
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        public event Action<User?>? CurrentUserChanged;

        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser;
            private set
            {
                if (_currentUser == value) return;
                _currentUser = value;
                CurrentUserChanged?.Invoke(value);
            }
        }

        public event Action<string?>? StatusMessageChanged;

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            private set
            {
                if (_statusMessage == value) return;
                _statusMessage = value;
                StatusMessageChanged?.Invoke(value);
            }
        }

        public void Exit()
        {
            CurrentUser = null;
            StatusMessage = null;
        }
    }
}
