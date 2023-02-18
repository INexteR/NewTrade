using MVVM.ViewModels;
using ShopModel;
using Interfaces;
using ShopModel.DTOs;
using System.Windows.Input;

namespace ShopViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Shop _shop;
        private readonly Locator _locator;

        public RelayCommand<LoginPassword> Login => GetCommand<LoginPassword>(LoginExecute, LoginCanExecute);

        public RelayCommand Guest => GetCommand(GuestExecute, GuestCanExecute);

        public LoginViewModel() // Конструктор для режима разработки
        {
            _shop = new();
            _locator = new()
            {
                CurrentViewModel = this
            };
        }

        public LoginViewModel(Shop shop, Locator locator)
        {
            _shop = shop;
            _locator = locator;
            _shop.Authorization.StatusChanged += OnStatusChanged;
        }

        private void OnStatusChanged(object? sender, AuthorizationStatus e)
        {
            AuthorizationStatus = e;
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
            ArgumentNullException.ThrowIfNull(loginPassword, nameof(loginPassword));
            var user = await Task.Run(() => _shop.Authorization.Login(loginPassword.Login, loginPassword.Password));
            if (user != null)
            {
                await Task.Run(NavigateToProducts);
            }
            else
            {
                loginExecuting = false;
                Login.RaiseCanExecuteChanged();
            }
        }

        private bool LoginCanExecute(LoginPassword loginPassword)
        {
            return !(loginExecuting || loginPassword.HasErrors);
        }

        private bool _guestExecuting;
        private async void GuestExecute(object? parameter)
        {
            _guestExecuting = true;
            Guest.RaiseCanExecuteChanged();
            _shop.Authorization.LoginAsGuest();
            await Task.Run(NavigateToProducts);          
        }


        private bool GuestCanExecute(object? parameter)
        {
            return !_guestExecuting;
        }

        private void NavigateToProducts()
        {
            _locator.CurrentViewModel = new ProductsViewModel(_shop, _locator);
        }

        public AuthorizationStatus AuthorizationStatus
        {
            get => Get<AuthorizationStatus>();
            private set => Set(value);
        }

        public override void Dispose()
        {
            _shop.Authorization.StatusChanged -= OnStatusChanged;
        }
    }
}
