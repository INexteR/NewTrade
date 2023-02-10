using System.ComponentModel;
using MVVM.ViewModels;

namespace ShopViewModels
{
    public class LoginPassword : Properties, IDataErrorInfo
    {
        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;

            set => Set(ref _password, value);
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                return columnName switch
                {
                    nameof(Login) => error.Validate(Login, loginError),
                    nameof(Password) => error.Validate(Password, passwordError),
                    _ => error
                };
            }
        }

        const string loginError = "Введите логин";
        const string passwordError = "Введите пароль";

        public string Error =>
            string.Empty.Validate(Login, $"{loginError}\n").Validate(Password, passwordError);

        public override bool HasErrors => !string.IsNullOrEmpty(Error);
    }

    file static class StringValidation
    {
        public static string Validate(this string error, string propValue, string message)
        {
            if (string.IsNullOrWhiteSpace(propValue))
            {
                error += message;
            }
            return error;
        }
    }
}
