
using MVVM.ViewModels;
using ShopViewModels;
using ViewModel;

namespace NewTrade
{
    public class Locator : ViewModelBase
    {
        public IAuthorizationViewModel? Authorization
        {
            get => Get<IAuthorizationViewModel?>();
            set => Set(value);
        }
        public ProductsViewModel? Products
        {
            get => Get<ProductsViewModel?>();
            set => Set(value);
        }
    }
}
