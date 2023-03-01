using Model;

namespace ShopSQLite.Entities
{
    internal partial class Pickuppoint : IPickupPoint
    {
        public Pickuppoint()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public string Index { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
