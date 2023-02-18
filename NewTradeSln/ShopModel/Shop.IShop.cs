using Interfaces;
using Microsoft.EntityFrameworkCore;
using ShopModel.DTOs;
using ShopModel.Entities;

namespace ShopModel
{
    public partial class Shop : IShop
    {

        public AuthorizationStatus Status { get; private set; }
        public IUser? CurrentUser { get; private set; }

        protected void SetAuthorizationStatus(AuthorizationStatus newStatus, IUser? newUser = null)
        {
            Status = newStatus;
            CurrentUser = newUser;
            AuthorizationChanged(this, new AuthorizationChangedArgs(newStatus, newUser));
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
            AuthorizationChanged(this, new AuthorizationChangedArgs(Status));
        }

        public void Login(string login, string password)
        {
            if (Status != AuthorizationStatus.None)
                throw new Exception($"Запрос новой авторизации возможен только в сотоянии {AuthorizationStatus.None}.");
            Status = AuthorizationStatus.InProcessing;
            AuthorizationChanged(this, new AuthorizationChangedArgs(Status));

            // Какие-то действия, потом проверка
            if (/* Условие прохождения авторизации */ true)
            {
            Status = AuthorizationStatus.Authorized;
            CurrentUser = new User();// 

            }    
        }

        public Task LoginAsync(string login, string password)
            => Task.Run(() => Login(login, password));

    }
}
