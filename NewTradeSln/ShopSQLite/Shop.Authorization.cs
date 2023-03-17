using Model;
using ShopSQLite.Entities;

namespace ShopSQLite
{
    public partial class Shop
    {
        public AuthorizationStatus Status { get; private set; }
        public IUser? CurrentUser { get; private set; }

        private void OnAuthorizationChanged()
        {
            AuthorizationChanged(this, new AuthorizationChangedArgs(Status, CurrentUser));
        }

        private readonly object authorizationChangedLocker = "Локер изменения состояния авторизации.";
        public event EventHandler<AuthorizationChangedArgs> AuthorizationChanged = (_, _) => { };

        public void Exit()
        {
            if (Status != AuthorizationStatus.Authorized)
                throw new Exception($"Выход возможен только из состояния {AuthorizationStatus.Authorized}.");

            // Какие-то действия.

            // Потом:
            lock (authorizationChangedLocker)
            {
                Status = AuthorizationStatus.None;
                CurrentUser = null;
                OnAuthorizationChanged();
            }
        }

        public void Authorize(string? login, string? password)
        {
            lock (authorizationChangedLocker)
            {
                if (Status is AuthorizationStatus.InProcessing or AuthorizationStatus.Authorized)
                    throw new Exception($"Запрос новой авторизации возможен в состояниях {AuthorizationStatus.None} и {AuthorizationStatus.Fail}.");
                Status = AuthorizationStatus.InProcessing;
                OnAuthorizationChanged();
            }

            Thread.Sleep(1_000);

            if (login is null && password is null)
            {
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.Authorized;
                    //CurrentUser = new User() { Name = "Гость" };
                    OnAuthorizationChanged();
                }
                return;
            }

            // Какие-то действия, потом проверка
            //byte[]? hash = ModelHelper.GetHashPassword(password);
            // Это временное решение. По нормальному нужно сравнивать хеши.
            User? user = catalog.Users.FirstOrDefault(user => user.Login == login && user.Password == password);

            if (user is not null)
            {
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.Authorized;
                    CurrentUser = user;// запоминание авторизированого пользователя.
                    OnAuthorizationChanged();
                }
            }
            else
            {
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.Fail;
                    OnAuthorizationChanged();
                }
            }
        }

        public Task AuthorizeAsync(string? login, string? password)
            => Task.Run(() => Authorize(login, password));

    }
}
