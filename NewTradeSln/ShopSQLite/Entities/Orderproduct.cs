using Model;

namespace ShopSQLite.Entities
{
    internal partial class Orderproduct : IOrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        IOrder IOrderProduct.Order => Order;
        IProduct IOrderProduct.Product => Product;
    }
}
