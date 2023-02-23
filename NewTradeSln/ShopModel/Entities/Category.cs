﻿namespace ShopModel.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        internal virtual ICollection<Product> Products { get; set; }
    }
}
