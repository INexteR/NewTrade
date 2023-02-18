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

        public string Name { get; } = "ООО «Ткани»";

        public event EventHandler<AuthorizationChangedArgs> AuthorizationChanged = (_, _) => { };

        public void Exit()
        {
            if (Status != AuthorizationStatus.Authorized)
                throw new Exception($"Выход возможен только из сотояния {AuthorizationStatus.Authorized}.");

            // Какие-то действия.

            // Потом:
            Status = AuthorizationStatus.None;
            CurrentUser = null;
            OnAuthorizationChanged();
        }

        public void Login(string login, string password)
        {
            if (Status != AuthorizationStatus.None)
                throw new Exception($"Запрос новой авторизации возможен только в сотоянии {AuthorizationStatus.None}.");
            Status = AuthorizationStatus.InProcessing;
            OnAuthorizationChanged();

            // Какие-то действия, потом проверка
            if (/* Условие прохождения авторизации */ true)
            {
                Status = AuthorizationStatus.Authorized;
                CurrentUser = new User();// запоминание авторизированого пользователя.
                OnAuthorizationChanged();
            }
            else
            {
                Status = AuthorizationStatus.None;
                OnAuthorizationChanged();
            }
        }

        public Task LoginAsync(string login, string password)
            => Task.Run(() => Login(login, password));

    }
}
