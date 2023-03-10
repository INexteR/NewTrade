namespace ShopSQLite.Entities
{
    [Table("units")]
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
