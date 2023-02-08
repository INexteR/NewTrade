using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTrade.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly Shop _shop;

        public ICommand Login { get; }

        public string Name => _shop.Name;

        public LoginViewModel(Shop shop)
        {
            _shop = shop;
            _shop.Authorization.StatusMessageChanged += OnStatusMessageChanged;
            Login = new AsyncRelayCommand<string[]>(LoginExecute);
        }

        private void OnStatusMessageChanged(string? statusMessage)
        {
            StatusMessage = statusMessage;
        }    

        private async Task LoginExecute(string[] data)
        {
            string login = data[0];
            string password = data[1];
            await _shop.Authorization.Login(login, password);
        }

        private string? _statusMessage;
        public string? StatusMessage
        {
            get => _statusMessage;
            private set => Set(ref _statusMessage, value);
        }
    }
}
