using Model;
using System.Collections.Generic;

namespace ViewModels
{
    public interface IManufacturersViewModel
    {
        IEnumerable<IManufacturer> Manufacturers { get; }
    }

}
