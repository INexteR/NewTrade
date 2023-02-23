using Interfaces;
using ShopModel.Entities;

namespace ShopModel
{
    public partial class Shop : IManufacturersSource
    {
        private readonly IReadOnlyCollection<Manufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> GetManufacturers()
            => manufacturers;
    }
}
