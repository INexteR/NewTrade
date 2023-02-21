
using Interfaces;
using MVVM.ViewModels;
using ShopModel;
using System.Collections.ObjectModel;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly IShop _shop;
        //private readonly IAuthorization _authorization;

        //public ICommand Exit => GetCommand(ExitExecute);

        // Это времменое поле. Пока нет нормального объявления интерфейса IShop
        private readonly Shop _shopTemp;

        public ProductsViewModel()
            : this(new Shop())
        { }

        public ProductsViewModel(IShop shop)
        {
            _shop = shop;
            _shopTemp = (Shop)shop;
            //_authorization = shop;
            //загрузка пока что в конструкторе
            Products = new ObservableCollection<ReadOnlyProductProxy>(_shopTemp.GetProducts().Select(p => new ReadOnlyProductProxy(p)) ?? Array.Empty<ReadOnlyProductProxy>());
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
        //public IUser? User => _authorization.CurrentUser;

        public ObservableCollection<ReadOnlyProductProxy> Products { get; }

        //private void ExitExecute(object? parameter)
        //{
        //    _authorization.Exit();

        //    // TODO: Локатор - скорее всего не нужен. Обычно Локатор - это сущность уровня View.
        //    //_locator.CurrentViewModel = new LoginViewModel(_authorization);
        //}
    }
}
