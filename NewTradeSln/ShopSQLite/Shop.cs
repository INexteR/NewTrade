
using Microsoft.EntityFrameworkCore;
using Model;

namespace ShopSQLite
{
    public partial class Shop : IShop
    {
        string сonnectionString = "sqliteTest.db";

        public Shop(bool recreate)
        {
            if (recreate)
            {
                if (File.Exists(сonnectionString))
                    File.Delete(сonnectionString);
            }
            var db = CatalogContext.Get(сonnectionString);
            db.Database.EnsureCreated();

        }
        public string Name { get; } = "ООО «Ткани»";

        public IEnumerable<IProduct> GetProducts()
        {
            using var context = CatalogContext.Get(сonnectionString);
            return context.Products.Include(p => p.Unit)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category).ToList();
            throw new NotImplementedException();
            //return TestData.GetProducts();
        }

        public IEnumerable<IManufacturer> GetManufacturers()
        {
            throw new NotImplementedException();
            //return TestData.GetManufacturers();
        }
    }
}
