namespace ShopModel.Entities
{
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        internal virtual ICollection<Product> Products { get; set; }
    }
}
