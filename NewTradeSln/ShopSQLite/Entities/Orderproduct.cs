using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("orderproduct")]
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    [Index(nameof(OrderId), Name = "order_idx")]
    [Index(nameof(ProductId), Name = "product_idx")]
    internal partial class OrderProduct : IOrderProduct
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
