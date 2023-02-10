using System.ComponentModel;
using MVVM.ViewModels;

namespace ShopViewModels
{
    public class LoginPassword : ValidationBase
    {
        public LoginPassword()
        {
            Login = string.Empty;
            Password = string.Empty;
        }

        public string Login
        {
            get => Get<string>()!;
            set => Set(value ?? string.Empty);
        }

        public string Password
        {
            get => Get<string>()!;

            set => Set(value ?? string.Empty);
        }

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string error = string.Empty;
        //        return columnName switch
        //        {
        //            nameof(Login) => error.Validate(Login, loginError),
        //            nameof(Password) => error.Validate(Password, passwordError),
        //            _ => error
        //        };
        //    }
        //}

        const string loginError = "Введите логин";
        const string passwordError = "Введите пароль";

        //public string Error =>
        //    string.Empty.Validate(Login, $"{loginError}\n").Validate(Password, passwordError);

        protected override void OnPropertyChanged(string propertyName, object? oldValue, object? newValue)
        {
            base.OnPropertyChanged(propertyName, oldValue, newValue);
            switch (propertyName)
            {
                case nameof(Login):
                    string? login = (string?)newValue;
                    if (string.IsNullOrWhiteSpace(login))
                        AddError(loginError, propertyName);
                    else
                        ClearErrors(propertyName);
                    break;
                case nameof(Password):
                    string? password = (string?)newValue;
                    if (string.IsNullOrWhiteSpace(password))
                        AddError(passwordError, propertyName);
                    else
                        ClearErrors(propertyName);
                    break;
            }
        }
    }

    //file static class StringValidation
    //{
    //    public static string Validate(this string error, string propValue, string message)
    //    {
    //        if (string.IsNullOrWhiteSpace(propValue))
    //        {
    //            error += message;
    //        }
    //        return error;
    //    }
    //}
}
