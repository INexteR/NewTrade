using CommonNet6.Collection;
using System.Collections.Generic;

namespace Model
{
    public interface IManufacturersSource
    {
        event NotifyListChangedEventHandler<IManufacturer> ManufacturerChanged;
        IReadOnlyCollection<IManufacturer> GetManufacturers();
    }
}
