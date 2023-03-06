using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("products")]
    [Index(nameof(CategoryId), Name = "category_idx")]
    [Index(nameof(ManufacturerId), Name = "manufacturer_idx")]
    [Index(nameof(SupplierId), Name = "supplier_idx")]
    [Index(nameof(UnitId), Name = "unit_idx")]
    internal partial class Product : IProduct
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        [Key]       
        public int Id { get; set; }
        [MaxLength(100)]
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
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        IUnit IProduct.Unit => Unit;
        IManufacturer IProduct.Manufacturer => Manufacturer;
        ISupplier IProduct.Supplier => Supplier;
        ICategory IProduct.Category => Category;
        IEnumerable<IOrderProduct> IProduct.OrderProducts => OrderProducts;
        
    }
}
