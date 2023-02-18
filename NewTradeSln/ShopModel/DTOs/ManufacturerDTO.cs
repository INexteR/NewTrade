
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class ManufacturerDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public ManufacturerDTO(Manufacturer manufacturer)
        {
            Id = manufacturer.IdManufacturer;
            Name = manufacturer.ManufacturerName;
        }
    }
}
