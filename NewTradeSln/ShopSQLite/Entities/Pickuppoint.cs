using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("pickuppoint")]
    internal partial class Pickuppoint : IPickupPoint
    {
        public Pickuppoint()
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
