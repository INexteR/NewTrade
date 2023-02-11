using MVVM.ViewModels;

namespace NewTrade
{
    public class Locator : ViewModelBase
    {
        public LoginViewModel? LoginViewModel { get => Get<LoginViewModel>(); set => Set(value); }
    }
}
