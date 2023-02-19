using Interfaces;
using ShopModel.Entities;
using ShopModel.Testing;

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

        public void Login(string? login, string? password)
        {
            lock (authorizationChangedLocker)
            {
                if (Status != AuthorizationStatus.None)
                    throw new Exception($"Запрос новой авторизации возможен только в состоянии {AuthorizationStatus.None}.");
                Status = AuthorizationStatus.InProcessing;
                OnAuthorizationChanged();
            }

            if (login is null && password is null) //гость
            {
                //а может быть вообще в случае гостя никаких действий не производить?
                //не вызывать метод Login(null, null), а просто сразу выполнить навигацию
                //установка статуса в авторизованного все равно никакого эффекта не оказывает
                Status = AuthorizationStatus.Authorized;
                return;
            }

            // Какие-то действия, потом проверка
            byte[]? hash = ModelHelper.GetHashPassword(password);
            var user = TestData.GetUsers()
                .FirstOrDefault(user => user.Login == login && user.HashPassword == hash);
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
                    Status = AuthorizationStatus.None;
                    OnAuthorizationChanged();
                }
            }
        }

        public Task LoginAsync(string? login, string? password)
            => Task.Run(() => Login(login, password));


        public IEnumerable<Product> GetProducts()
        {
            //using var context = Context.Get();
            //return context.Products.Include(p => p.ProductUnitNavigation)
            //    .Include(p => p.ProductManufacturerNavigation)
            //    .Include(p => p.ProductSupplierNavigation)
            //    .Include(p => p.ProductCategoryNavigation).ToList();
            return TestData.GetProducts();
        }

    }
}
