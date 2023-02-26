using Model;
using System.Collections.Generic;

namespace ViewModel
{
    public interface IManufacturersSourceViewModel
    {
        IReadOnlyCollection<IManufacturer> Manufacturers { get; }
    }

}
