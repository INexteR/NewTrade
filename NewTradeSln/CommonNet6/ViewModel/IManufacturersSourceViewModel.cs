using Model;
using System.Collections.Generic;

namespace ViewModel
{
    public interface IManufacturersViewModel
    {
        IReadOnlyCollection<IManufacturer> Manufacturers { get; }
    }

}
