using System;
using System.Collections.Generic;

namespace ShopModel.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public string ProductArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ProductUnit { get; set; }
        public decimal ProductCost { get; set; }
        public int ProductManufacturer { get; set; }
        public int ProductSupplier { get; set; }
        public int ProductCategory { get; set; }
        public int ProductMaxDiscountAmount { get; set; }
        public sbyte? ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductDescription { get; set; } = null!;
        public byte[]? ProductPhoto { get; set; }

        public virtual Category ProductCategoryNavigation { get; set; } = null!;
        public virtual Supplier ProductManufacturerNavigation { get; set; } = null!;
        public virtual Manufacturer ProductSupplierNavigation { get; set; } = null!;
        public virtual Unit ProductUnitNavigation { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
