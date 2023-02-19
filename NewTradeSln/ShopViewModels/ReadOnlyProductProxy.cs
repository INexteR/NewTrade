using ShopModel.Entities;

namespace ShopViewModels
{
    public class ReadOnlyProductProxy
    {
        private readonly Product _product;

        public ReadOnlyProductProxy(Product product)
        {
            _product = product;
        }

        public string Name => _product.Name;
        public Unit Unit => /*_product.Unit*/null;
        public decimal Cost => _product.Cost;
        public Manufacturer Manufacturer => /*_product.Manufacturer*/null;
        public Supplier Supplier => /*_product.Supplier*/null;
        public Category Category => /*_product.Category*/null;
        public int MaxDiscountAmount => _product.MaxDiscountAmount;
        public sbyte? DiscountAmount => _product.DiscountAmount;
        public int QuantityInStock => _product.QuantityInStock;
        public string Description => /*_product.Description*/null;
        public string? Path => _product.Path;
    }
}
