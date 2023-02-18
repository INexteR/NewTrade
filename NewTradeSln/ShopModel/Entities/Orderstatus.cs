using System;
using System.Collections.Generic;

namespace ShopModel.Entities
{
    public partial class Orderstatus
    {
        public Orderstatus()
        {
            Orders = new HashSet<Order>();
        }

        public int IdOrderStatus { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
