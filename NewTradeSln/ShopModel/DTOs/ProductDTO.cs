
using ShopModel.Entities;

namespace ShopModel.DTOs
{
    public class ProductDTO
    {
        public string ArticleNumber { get; }
        public string Name { get; set; }
        public UnitDTO Unit { get; set; }
        public decimal Cost { get; set; }
        public ManufacturerDTO Manufacturer { get; set; }
        public SupplierDTO Supplier { get; set; }
        public CategoryDTO Category { get; set; }
        public int MaxDiscountAmount { get; set; }
        public sbyte? DiscountAmount { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public string? Path { get; set; }

        public ProductDTO(Product p)
        {
            ArticleNumber = p.ProductArticleNumber;
            Name = p.ProductName;
            Unit = new(p.ProductUnitNavigation);
            Cost = p.ProductCost;
            Manufacturer = new(p.ProductManufacturerNavigation);
            Supplier = new(p.ProductSupplierNavigation);
            Category = new(p.ProductCategoryNavigation);
            MaxDiscountAmount = p.ProductMaxDiscountAmount;
            DiscountAmount = p.ProductDiscountAmount;
            QuantityInStock = p.ProductQuantityInStock;
            Description = p.ProductDescription;
            Path = p.ProductPhoto ?? "/Resources/picture.png";
        }
    }
}
