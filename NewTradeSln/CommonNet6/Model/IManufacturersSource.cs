using System.Collections.Generic;

namespace Model
{
    public interface IManufacturersSource
    {
        IReadOnlyCollection<IManufacturer> GetManufacturers();
    }
}
