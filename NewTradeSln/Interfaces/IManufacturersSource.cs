
namespace Interfaces
{
    public interface IManufacturersSource
    {
        IReadOnlyCollection<IManufacturer> GetManufacturers();
    }
}
