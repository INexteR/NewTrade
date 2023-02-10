using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM.ViewModels;
using MVVM.Commands;
using System.Windows.Input;
using System.ComponentModel;
using ShopModel;

namespace ShopViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Shop _shop;

        public RelayCommand Login { get; }

        public string Name => _shop.Name;
        public LoginViewModel() // Конструктор для режима разработки
        {
            Login = new RelayCommand<LoginPassword>(LoginExecute, LoginCanExecute);
            _shop = new Shop();
        }

        public LoginViewModel(Shop shop)
        {
            _shop = shop;
            _shop.Authorization.StatusMessageChanged += OnStatusMessageChanged;
            Login = new RelayCommand<LoginPassword>(LoginExecute, LoginCanExecute);
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
            return !(loginExecuting || (loginPassword?.HasErrors ?? false));
        }

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            private set => Set(ref _statusMessage, value);
        }
    }
}
