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

        public ICommand Login { get; }

        public string Name => _shop.Name;

        public LoginViewModel(Shop shop, LoginPassword lp)
        {
            _shop = shop;
            _shop.Authorization.StatusMessageChanged += OnStatusMessageChanged;
            Login = new AsyncRelayCommand<Properties>(LoginExecute, LoginCanExecute, lp, nameof(LoginPassword.Login), nameof(LoginPassword.Password));
        }

        private void OnStatusMessageChanged(string? statusMessage)
        {
            StatusMessage = statusMessage;
        }

        private async Task LoginExecute(Properties properties)
        {
            string login = properties.Get<string>("Login");
            string password = properties.Get<string>("Password");
            await _shop.Authorization.Login(login, password);
        }

        private bool LoginCanExecute(Properties properties)
        {
            return !properties.HasErrors;
        }

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            private set => Set(ref _statusMessage, value);
        }
    }
}
