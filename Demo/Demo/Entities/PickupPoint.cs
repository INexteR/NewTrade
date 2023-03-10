namespace ShopSQLite.Entities
{
    [Table("pickuppoint")]
    public partial class PickupPoint
    {
        public PickupPoint()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Index { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
