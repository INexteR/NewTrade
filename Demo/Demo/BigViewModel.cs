namespace Demo
{
    public class BigViewModel : BaseInpc
    {
        private const string dataBaseName = "sqliteTest.db";
        private readonly CatalogContext catalog = new(dataBaseName);

        public void Init(bool recreate = false)
        {
            if (recreate)
            {
                if (File.Exists(dataBaseName))
                    File.Delete(dataBaseName);
            }
            catalog.Database.EnsureCreated();
            catalog.Manufacturers.Load();
            catalog.Products.Include(p => p.Unit).Include(p => p.Manufacturer)
                .Include(p => p.Supplier).Include(p => p.Category)
                .Include(p => p.OrderProducts).Load();
        }

        public User? User { get; private set; }

        public User? Authorize(string login, string password)
        {
            foreach (var user in catalog.Users)
            {
                if (user.Login == login && user.Password == password)
                {
                    return User = user;
                }
            }
            return null;
        }

        public void Exit()
        {
            User = null;
        }

        public ObservableCollection<Manufacturer> Manufacturers => catalog.Manufacturers.Local.ToObservableCollection();

        public ObservableCollection<Product> Products => catalog.Products.Local.ToObservableCollection();

    }
}
