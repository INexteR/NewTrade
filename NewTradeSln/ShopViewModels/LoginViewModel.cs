using Interfaces;
using MVVM.ViewModels;
using ShopModel;

namespace ShopViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthorization _authorization;

        #region Прокси-свойства
        public IUser? CurrentUser => _authorization.CurrentUser;
        public AuthorizationStatus CurrentStatus => _authorization.Status;
        #endregion

        public RelayCommand<LoginPassword> Authorize => GetCommand<LoginPassword>(AuthorizeExecute, AuthorizeCanExecute);

        public RelayCommand Guest => GetCommand(GuestExecute, GuestCanExecute);

        public LoginViewModel() // Конструктор для режима разработки
        {
            _authorization = new Shop();
        }

        public LoginViewModel(IAuthorization authorization)
        {
            _authorization = authorization;
            _authorization.AuthorizationChanged += OnAuthorizationChanged;
        }

        private void OnAuthorizationChanged(object? sender, AuthorizationChangedArgs e)
        {
            // Уведомление об изменении прокси-свойств.
            RaisePropertyChanged(nameof(CurrentStatus));
            RaisePropertyChanged(nameof(CurrentUser));
        }
        private async void AuthorizeExecute(LoginPassword loginPassword)
        {
            AuthorizeExecuting = true;
            await _authorization.LoginAsync(loginPassword.Login, loginPassword.Password);
            AuthorizeExecuting = false;
        }

        private bool AuthorizeExecuting
        {
            get => Get<bool>();
            set 
            {
                Set(value);
                Authorize.RaiseCanExecuteChanged();
            }
        }

        private bool AuthorizeCanExecute(LoginPassword loginPassword)
        {
            return !(loginPassword.HasErrors || AuthorizeExecuting)
                && _authorization.Status == AuthorizationStatus.None;
        }
        private async void GuestExecute()
        {
            await _authorization.LoginAsync(null, null);
        }


        private bool GuestCanExecute()
        {
            return _authorization.Status == AuthorizationStatus.None;
        }
    }
}
