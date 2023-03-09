namespace ShopSQLite.Entities
{
    [Table("orderstatuses")]
    internal partial class Orderstatus
    {
        public Orderstatus()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
