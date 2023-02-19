
using Interfaces;
using MVVM.ViewModels;
using ShopModel;
using ShopModel.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly IShop _shop;
        private readonly IAuthorization _authorization;
        private readonly Locator _locator;

        public ICommand Exit => GetCommand(ExitExecute);

        public ProductsViewModel(Shop shop, Locator locator)
        {
            _shop = shop;
            _authorization = shop;
            _locator = locator;
            //загрузка пока что в конструкторе
            Products = new ObservableCollection<ReadOnlyProductProxy>(shop.GetProducts().Select(p => new ReadOnlyProductProxy(p)));
        }

        public string Name => _shop.Name;

        //public string Username
        //{
        //    get
        //    {
        //        var user = _shop.Authorization.CurrentUser;
        //        if (user != null)
        //            return $"{user.Surname} {user.Name} {user.Patronymic}";
        //        return "Гость";
        //    }
        //}
        public IUser? User => _authorization.CurrentUser;

        public IEnumerable<ReadOnlyProductProxy> Products { get; }

        private void ExitExecute(object? parameter)
        {
            _authorization.Exit();

            // TODO: Локатор - скорее всего не нужен. Обычно Локатор - это сущность уровня View.
            _locator.CurrentViewModel = new LoginViewModel(_authorization);
        }
    }
}
