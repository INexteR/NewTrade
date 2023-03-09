namespace Demo.VMs
{
    class ProductsVM : BaseInpc
    {
        public User? User { get; }

        public string Name { get; }

        public ObservableCollection<Manufacturer> Manufacturers { get; }

        private static Manufacturer[] GetManufacturers()
        {
            using var context = CatalogContext.Get();
            return context.Manufacturers.ToArray();
        }

        public ObservableCollection<Product> Products { get; }

        private Product[] GetProducts()
        {
            using var context = CatalogContext.Get();
            context.Manufacturers.AttachRange(Manufacturers);
            return context.Products.Include(p => p.Unit).Include(p => p.Manufacturer)
                .Include(p => p.Supplier).Include(p => p.Category).ToArray();
        }

        public ProductsVM(User? user)
        {
            User = user;
            Name = "ООО «Ткани»";
            Manufacturers = new(GetManufacturers());
            Products = new(GetProducts());
        }
    }
}
