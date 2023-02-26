using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("products")]
    internal partial class Product : IProduct
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        [Key]
        public int Id { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int UnitId { get; set; }
        public decimal Cost { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public int MaxDiscountAmount { get; set; }
        public sbyte? DiscountAmount { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; } = null!;
        public string? Path { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual Unit Unit { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }

        IManufacturer IProduct.Manufacturer => Manufacturer;
    }
}
