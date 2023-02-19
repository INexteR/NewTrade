using Interfaces;
using ShopModel.Entities;

namespace ShopModel
{
    public partial class Shop : IShop
    {

        public AuthorizationStatus Status { get; private set; }
        public IUser? CurrentUser { get; private set; }

        private void OnAuthorizationChanged()
        {
            AuthorizationChanged(this, new AuthorizationChangedArgs(Status, CurrentUser));
        }

        private readonly object authorizationChangedLocker = "Локер изменения состояния авторизации.";

        public string Name { get; } = "ООО «Ткани»";

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

        public void Login(string login, string password)
        {
            lock (authorizationChangedLocker)
            {
                if (Status != AuthorizationStatus.None)
                    throw new Exception($"Запрос новой авторизации возможен только в состоянии {AuthorizationStatus.None}.");
                Status = AuthorizationStatus.InProcessing;
                OnAuthorizationChanged();
            }

            // Какие-то действия, потом проверка
            byte[]? hash = ModelHelper.GetHashPassword(password);
            var user = Context
                .Get(true)
                .Users
                .FirstOrDefault(user => user.Login == login && user.HashPassword == hash);
            if (user is not null)
            {
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.Authorized;
                    CurrentUser = new User();// запоминание авторизированого пользователя.
                    OnAuthorizationChanged();
                }
            }
            else
            {
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.None;
                    OnAuthorizationChanged();
                }
            }
        }

        public Task LoginAsync(string login, string password)
            => Task.Run(() => Login(login, password));

    }
}
