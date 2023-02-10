using System;
using System.Collections.Generic;

namespace ShopModel.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        public int IdUnit { get; set; }
        public string UnitName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
