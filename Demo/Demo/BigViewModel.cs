namespace Demo
{
    public class BigViewModel : BaseInpc
    {
        private const string dataBaseName = "sqliteTest.db";
        private readonly CatalogContext catalog = new(dataBaseName);

        public static BigViewModel Instance { get; } = new();
        private BigViewModel()
        {
            Manufacturers = catalog.Manufacturers.Local.ToObservableCollection();
            Products = catalog.Products.Local.ToObservableCollection();
        }

        public void Init(bool recreate = false)
        {
            if (recreate)
            {
                if (File.Exists(dataBaseName))
                    File.Delete(dataBaseName);
            }
            catalog.Database.EnsureCreated();

            // Если все сущности загружаются, то не нужно делать Include()
            catalog.Roles.Load();
            catalog.Categories.Load();
            catalog.Manufacturers.Load();
            catalog.Suppliers.Load();
            catalog.Units.Load();
            catalog.Users.Load();
            catalog.Products.Load();
            catalog.OrderStatuses.Load();
            catalog.PickupPoints.Load();
            catalog.Orders.Load();
            catalog.OrderProducts.Load();
        }

        public User? User { get; private set; }

        public User? Authorize(string login, string password)
            => catalog.Users.Local.FirstOrDefault(user => user.Login == login && user.Password == password);

        public void Exit()
        {
            User = null;
        }

        public void Save(Product product)
        {
            MessageBox.Show("Сохранить product.");
        }

        public ObservableCollection<Manufacturer> Manufacturers { get; }

        public ObservableCollection<Product> Products { get; }
    }
}
