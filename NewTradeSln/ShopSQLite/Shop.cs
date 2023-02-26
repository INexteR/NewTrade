
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.ObjectModel;

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
            manufacturers = new ReadOnlyCollection<IManufacturer>(db.Manufacturers.ToArray());
        }
        public string Name { get; } = "ООО «Ткани»";

        public IList<IProduct> GetProducts()
        {
            using var context = CatalogContext.Get(сonnectionString);
            return context.Products.Include(p => p.Unit)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category).ToArray();
        }

        private readonly ReadOnlyCollection<IManufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> GetManufacturers()
            => manufacturers;
    }
}
