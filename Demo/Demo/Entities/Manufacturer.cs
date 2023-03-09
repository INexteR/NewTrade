namespace ShopSQLite.Entities
{
    [Table("manufacturers")]
    internal partial class Manufacturer
    {
        public Manufacturer()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
