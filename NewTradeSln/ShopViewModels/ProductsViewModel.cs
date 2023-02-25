
using MVVM.ViewModels;
using ShopModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly IShop _shop;

        public ProductsViewModel(IShop shop)
        {
            _shop = shop;
            //загрузка пока что в конструкторе, это плохо, надо как-то решать
            Products = new ObservableCollection<IProduct>(_shop.GetProducts());
            Manufacturers = new ObservableCollection<IManufacturer>(_shop.GetManufacturers());
        }

        public string Name => _shop.Name;

        public ObservableCollection<IProduct> Products { get; }

        public ObservableCollection<IManufacturer> Manufacturers { get; }
    }
}
