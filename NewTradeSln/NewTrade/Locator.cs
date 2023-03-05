using System.ComponentModel;
using ViewModels;

namespace NewTrade
{
    public class Locator : ViewModelBase
    {
        public IAuthorizationViewModel? Authorization
        {
            get => Get<IAuthorizationViewModel?>();
            set => Set(value);
        }
        //public IManufacturersViewModel? ManufacturersSource
        //{
        //    get => Get<IManufacturersViewModel?>();
        //    set => Set(value);
        //}
        public IProductsViewModel? Products
        {
            get => Get<IProductsViewModel?>();
            set => Set(value);
        }
    }
}
