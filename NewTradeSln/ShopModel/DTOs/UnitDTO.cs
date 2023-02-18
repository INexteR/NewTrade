using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class UnitDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public UnitDTO(Unit unit)
        {
            Id = unit.IdUnit;
            Name = unit.UnitName;
        }
    }
}
