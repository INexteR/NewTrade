using Model;
using System.Collections.Generic;

namespace ViewModels
{
    public interface ISourcesViewModel
    {
        IEnumerable<IManufacturer> Manufacturers { get; }

        IEnumerable<ISupplier> Suppliers { get; }

        IEnumerable<IUnit> Units { get; }

        IEnumerable<ICategory> Categories { get; }

    }

}
