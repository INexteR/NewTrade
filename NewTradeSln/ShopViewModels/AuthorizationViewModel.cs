using Model;
using ViewModels;
using ShopSQLite;
using System.Windows.Input;

namespace ShopViewModels
{
    public class AuthorizationViewModel : ViewModelBase, IAuthorizationViewModel
    {
        // Модель авторизации.
        private readonly IAuthorization _authorization;

        #region Прокси-свойства
        public IUser? CurrentUser => _authorization.CurrentUser;
        public AuthorizationStatus CurrentStatus => _authorization.Status;
        #endregion

        #region Команда авторизированного входа.
        /// <inheritdoc cref="IAuthorizationViewModel.Authorize"/>
        public RelayCommand<LoginPassword> Authorize => GetCommand<LoginPassword>(AuthorizeExecute, AuthorizeCanExecute);
        ICommand IAuthorizationViewModel.Authorize => Authorize;

        private bool AuthorizeCanExecute(LoginPassword loginPassword)
        {
            return !loginPassword.HasErrors &&
                _authorization.Status is AuthorizationStatus.None or AuthorizationStatus.Fail;
        }
        private async void AuthorizeExecute(LoginPassword loginPassword)
        {
            await _authorization.AuthorizeAsync(loginPassword.Login, loginPassword.Password);
        }
        #endregion 

        #region Команда гостевого входа.
        /// <inheritdoc cref="IAuthorizationViewModel.Guest"/>
        public RelayCommand Guest => GetCommand(GuestExecute, GuestCanExecute);

        ICommand IAuthorizationViewModel.Guest => Guest;
        private async void GuestExecute()
        {
            await _authorization.AuthorizeAsync(null, null);
        }

        private bool GuestCanExecute()
        {
            return _authorization.Status is AuthorizationStatus.None 
                or AuthorizationStatus.Fail;
        }
        #endregion 

        #region Команда выхода из авторизации.
        /// <inheritdoc cref="IAuthorizationViewModel.Exit"/>
        public RelayCommand Exit => GetCommand(ExitExecute, ExitCanExecute);

        ICommand IAuthorizationViewModel.Exit => Exit;

        private bool ExitCanExecute() => _authorization.Status == AuthorizationStatus.Authorized;

        private void ExitExecute() => _authorization.Exit();
        #endregion 

        public AuthorizationViewModel() // Конструктор для режима разработки
            : this(new Shop(true))
        { }

        public AuthorizationViewModel(IAuthorization authorization)
        {
            _authorization = authorization;
            _authorization.AuthorizationChanged += OnAuthorizationChanged;
        }

        private void OnAuthorizationChanged(object? sender, AuthorizationChangedArgs e)
        {
            // Уведомление об изменении прокси-свойств.
            RaisePropertyChanged(nameof(CurrentStatus));
            RaisePropertyChanged(nameof(CurrentUser));

            // Уведомление об изменении состояния команд.
            Authorize.RaiseCanExecuteChanged();
            Guest.RaiseCanExecuteChanged();
            Exit.RaiseCanExecuteChanged();
        }


    }
}
