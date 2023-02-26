using Model;
using MVVM.ViewModels;
using System.Collections.ObjectModel;
using ViewModel;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase, IManufacturersSourceViewModel
    {
        private readonly IShop _shop;

        public ProductsViewModel(IShop shop)
        {
            _shop = shop;
            //загрузка пока что в конструкторе, это плохо, надо как-то решать
            Products = new ObservableCollection<IProduct>(_shop.GetProducts());
            manufacturers = new(_shop.GetManufacturers().ToArray());
        }

        public string Name => _shop.Name;

        public ObservableCollection<IProduct> Products { get; }

        private readonly ReadOnlyCollection<IManufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> Manufacturers => manufacturers;

    }
}
