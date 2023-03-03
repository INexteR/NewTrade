using Model;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface IManufacturersViewModel
    {
        ObservableCollection<IManufacturer> Manufacturers { get; }
    }

}
