namespace ShopModel.Entities
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        internal virtual ICollection<Product> Products { get; set; }
    }
}
