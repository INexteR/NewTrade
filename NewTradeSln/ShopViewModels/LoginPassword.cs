using MVVM.ViewModels;

namespace ShopViewModels
{
    public class LoginPassword
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    //public class LoginPassword : ValidationBase
    //{
    //    public LoginPassword()
    //    {
    //        Login = string.Empty;
    //        Password = string.Empty;
    //    }

    //    public string Login
    //    {
    //        get => Get<string>()!;
    //        set => Set(value ?? string.Empty);
    //    }

    //    public string Password
    //    {
    //        get => Get<string>()!;

    //        set => Set(value ?? string.Empty);
    //    }

    //    const string loginError = "Введите логин";
    //    const string passwordError = "Введите пароль";


    //    protected override void OnPropertyChanged(string propertyName, object? oldValue, object? newValue)
    //    {
    //        base.OnPropertyChanged(propertyName, oldValue, newValue);
    //        switch (propertyName)
    //        {
    //            case nameof(Login):
    //                string? login = (string?)newValue;
    //                if (string.IsNullOrWhiteSpace(login))
    //                    AddError(loginError, propertyName);
    //                else
    //                    ClearErrors(propertyName);
    //                break;
    //            case nameof(Password):
    //                string? password = (string?)newValue;
    //                if (string.IsNullOrWhiteSpace(password))
    //                    AddError(passwordError, propertyName);
    //                else
    //                    ClearErrors(propertyName);
    //                break;
    //        }
    //    }
    //}

}
