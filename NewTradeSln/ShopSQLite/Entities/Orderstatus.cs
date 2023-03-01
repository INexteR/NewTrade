using Model;

namespace ShopSQLite.Entities
{
    internal partial class Orderstatus : IOrderStatus
    {
        public Orderstatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
