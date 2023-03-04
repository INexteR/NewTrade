using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public interface IProductsViewModel : IManufacturersViewModel
    {
        string Name { get; }

        ObservableCollection<IProduct> Products { get; }

        ICommand Remove { get; }
    }

}
