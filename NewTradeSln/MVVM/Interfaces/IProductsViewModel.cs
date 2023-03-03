using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface IProductsViewModel : IManufacturersViewModel
    {
        string Name { get; }
        ObservableCollection<IProduct> Products { get; }

        //RelayCommand
    }

}
