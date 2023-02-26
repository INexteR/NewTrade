namespace Model
{
    public interface IProduct
    {
        string ArticleNumber { get; }
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

        IManufacturer Manufacturer { get; }
    }
}
