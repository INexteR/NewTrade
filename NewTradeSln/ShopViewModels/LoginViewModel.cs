using MVVM.ViewModels;
using ShopModel;

namespace ShopViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Shop _shop;

        public RelayCommand<LoginPassword> Login => GetCommand<LoginPassword>(LoginExecute, LoginCanExecute);

        public string Name => _shop.Name;
        public LoginViewModel() // Конструктор для режима разработки
        {
            _shop = new Shop();
        }

        public LoginViewModel(Shop shop)
        {
            _shop = shop;
            _shop.Authorization.StatusMessageChanged += OnStatusMessageChanged;
        }

        private void OnStatusMessageChanged(string? statusMessage)
        {
            StatusMessage = statusMessage;
        }

        private bool loginExecuting;
        private async void LoginExecute(LoginPassword loginPassword)
        {
            if (loginExecuting)
            {
                throw new Exception("Авторизация уже выполняется");
            }
            loginExecuting = true;
            Login.RaiseCanExecuteChanged();

            if (loginPassword == null)
            {
                throw new ArgumentNullException(nameof(loginPassword));
            }
            await _shop.Authorization.Login(loginPassword.Login, loginPassword.Password);
            loginExecuting = false;
            Login.RaiseCanExecuteChanged();
        }

        private bool LoginCanExecute(LoginPassword loginPassword)
        {
            return !(loginExecuting ||
                     string.IsNullOrWhiteSpace(loginPassword?.Login) ||
                     string.IsNullOrWhiteSpace(loginPassword.Password));
        }

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            private set => Set(ref _statusMessage, value);
        }
    }
}
