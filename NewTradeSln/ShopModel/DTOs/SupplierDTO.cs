
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; }

        public string Name { get; set; }

        public SupplierDTO(Supplier supplier)
        {
            Id = supplier.IdSupplier;
            Name = supplier.SupplierName;
        }
    }
}
