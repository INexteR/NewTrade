using System.Collections.Generic;

namespace Model
{
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        int UnitId { get; }
        decimal Cost { get; }
        int ManufacturerId { get; }
        int SupplierId { get; }
        int CategoryId { get; }
        int MaxDiscountAmount { get; }
        sbyte? DiscountAmount { get; }
        int QuantityInStock { get; }
        string Description { get; }
        string? Path { get; }

        IUnit Unit { get; }
        IManufacturer Manufacturer { get; }
        ISupplier Supplier { get; }
        ICategory Category { get; }
        IEnumerable<IOrderProduct> OrderProducts { get; }
    }
}
