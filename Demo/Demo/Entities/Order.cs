namespace ShopSQLite.Entities
{
    [Table("orders")]
    [Index(nameof(PickupPointId), Name = "pickuppoint_idx")]
    [Index(nameof(OrderStatusId), Name = "orderstatus_idx")]
    internal partial class Order
    {
        public Order()
        {
            Orderproducts = new HashSet<OrderProduct>();
        }

        [Key]
        public int Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public int PickupPointId { get; set; }
        public string? ClientName { get; set; }
        public int CodeToGet { get; set; }
        public int OrderStatusId { get; set; }

        public virtual Orderstatus OrderStatus { get; set; } = null!;
        public virtual PickupPoint PickupPoint { get; set; } = null!;
        public virtual ICollection<OrderProduct> Orderproducts { get; set; }

    }
}
