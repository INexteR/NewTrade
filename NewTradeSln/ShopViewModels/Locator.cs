using MVVM.ViewModels;

namespace ShopViewModels
{
    public class Locator : ViewModelBase
    {
        public ViewModelBase? CurrentViewModel
        {
            get => Get<ViewModelBase?>();
            set
            {
                CurrentViewModel?.Dispose();
                Set(value);
            }
        }
    }
}
