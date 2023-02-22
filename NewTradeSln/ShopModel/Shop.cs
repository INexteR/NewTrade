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

        public void Authorize(string? login, string? password)
        {
            lock (authorizationChangedLocker)
            {
                if (Status is AuthorizationStatus.InProcessing or AuthorizationStatus.Authorized)
                    throw new Exception($"Запрос новой авторизации возможен в состояниях {AuthorizationStatus.None} и {AuthorizationStatus.Fail}.");
                Status = AuthorizationStatus.InProcessing;
                OnAuthorizationChanged();
            }

            Thread.Sleep(1_000); // Имитация долгой обработки.

            if (login is null) //гость
            {
                //а может быть вообще в случае гостя никаких действий не производить?
                //не вызывать метод Login(null, null), а просто сразу выполнить навигацию
                //установка статуса в авторизованного все равно никакого эффекта не оказывает

                // Нет! VM отражает Модель. Сама по себе она ничего не делает.
                // Тем более, что у гостя тоже может быть имя. 
                lock (authorizationChangedLocker)
                {
                    Status = AuthorizationStatus.Authorized;
                    CurrentUser = new User() { Name = "Гость 12345" };
                    OnAuthorizationChanged();
                }
                return;
            }

            // Какие-то действия, потом проверка
            //var user = TestData.GetUsers()
            //   .FirstOrDefault(user => user.Login == login && user.CheckPassword(password));


            // Для БД нужно так проверять:
            byte[]? hash = ModelHelper.GetHashPassword(password);
            User? user;
            using (var db = ShopContext.Get())
            {
                user = db.Users.FirstOrDefault(user => user.Login == login);
            }

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
