namespace ShopSQLite.Entities
{
    [Table("suppliers")]
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
