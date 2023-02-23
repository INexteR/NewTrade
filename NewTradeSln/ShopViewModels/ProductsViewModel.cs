
using Interfaces;
using MVVM.ViewModels;
using ShopModel;
using System.Collections.ObjectModel;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private readonly IShop _shop;

        // Это времменое поле. Пока нет нормального объявления интерфейса IShop
        private readonly Shop _shopTemp;

        public ProductsViewModel()
            : this(new Shop())
        { }

        public ProductsViewModel(IShop shop)
        {
            _shopTemp = (Shop)shop; // Удалить после полной реализации всех интерфейсов
           

            _shop = shop;


            //загрузка пока что в конструкторе
            Products = new ReadOnlyCollection<IProduct>(_shopTemp.GetProducts().ToArray());
            Manufacturers = new ReadOnlyCollection<IManufacturer>(_shop.GetManufacturers().ToArray());
        }

        public string Name => _shop.Name;

        public IReadOnlyCollection<IProduct> Products { get; }
        public IReadOnlyCollection<IManufacturer> Manufacturers { get; }

    }
}
