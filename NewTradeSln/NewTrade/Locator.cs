
using MVVM.ViewModels;
using ShopSQLite;
using ShopViewModels;
using System.ComponentModel;
using ViewModel;

namespace NewTrade
{
    public class Locator : ViewModelBase
    {
        public bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new());
        public IAuthorizationViewModel? Authorization
        {
            get => Get<IAuthorizationViewModel?>();
            set => Set(value);
        }
        public IManufacturersViewModel? ManufacturersSource
        {
            get => Get<IManufacturersViewModel?>();
            set => Set(value);
        }
        public IProductsViewModel? Products
        {
            get => Get<IProductsViewModel?>();
            set => Set(value);
        }
        public Locator()
        {
            if (IsInDesignMode)
            {
                // Здесь инициализация для режима разработки.

                var shop = new Shop(true);
                Authorization = new AuthorizationViewModel(shop);
                Products = new ProductsViewModel(shop);
                ManufacturersSource = Products;
            }
        }
    }
}
