
using ShopModel.DTOs;

namespace ShopViewModels
{
    public class ReadOnlyProductProxy
    {
        private readonly ProductDTO _product;

        public ReadOnlyProductProxy(ProductDTO product)
        {
            _product = product;
        }

        public string Name => _product.Name;
        public UnitDTO Unit => _product.Unit;
        public decimal Cost => _product.Cost;
        public ManufacturerDTO Manufacturer => _product.Manufacturer;
        public SupplierDTO Supplier => _product.Supplier;
        public CategoryDTO Category => _product.Category;
        public int MaxDiscountAmount => _product.MaxDiscountAmount;
        public sbyte? DiscountAmount => _product.DiscountAmount;
        public int QuantityInStock => _product.QuantityInStock;
        public string Description => _product.Description;
        public string? Path => _product.Path;
    }
}
