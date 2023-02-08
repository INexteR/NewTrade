using System;
using System.Collections.Generic;

namespace NewTrade.Model.DTOs
{
    public partial class Order
    {
        public Order()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public int OrderId { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly OrderDeliveryDate { get; set; }
        public int OrderPickupPoint { get; set; }
        public string? ClientName { get; set; }
        public int CodeToGet { get; set; }
        public int OrderStatus { get; set; }

        public virtual Pickuppoint OrderPickupPointNavigation { get; set; } = null!;
        public virtual Orderstatus OrderStatusNavigation { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
