
namespace Interfaces
{
    public interface IManufacturersSource
    {
        IReadOnlyCollection<IManufacturer> GetManufacturers();

        event EventHandler<ManufacturersChangedArgs> ManufacturersChanged;
    }
}
