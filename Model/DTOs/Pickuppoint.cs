using System;
using System.Collections.Generic;

namespace NewTrade.Model.DTOs
{
    public partial class Pickuppoint
    {
        public Pickuppoint()
        {
            Orders = new HashSet<Order>();
        }

        public int IdPickupPoint { get; set; }
        public string PickupPointAddress { get; set; } = null!;
        public string PickupPointIndex { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
