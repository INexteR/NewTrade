using System.Collections;
using ShopSQLite.Entities;
using Model;
using Microsoft.EntityFrameworkCore;
using CommonNet6.Collection;

namespace ShopSQLite
{
    public partial class Shop : IShop
    {
        const string сonnectionString = "sqliteTest.db";

        public Shop(bool recreate = false)
        {
            if (recreate)
            {
                if (File.Exists(сonnectionString))
                    File.Delete(сonnectionString);
            }
            var db = CatalogContext.Get(сonnectionString);
            db.Database.EnsureCreated();
        }

        public Task LoadDataAsync() => Task.Run(() =>
        {
            using (var context = CatalogContext.Get(сonnectionString))
            {
                units.AddRange(context.Units.ToList());
                manufacturers.AddRange(context.Manufacturers.ToList());
                suppliers.AddRange(context.Suppliers.ToList());
                categories.AddRange(context.Categories.ToList());
                products.AddRange(context.Products.Include(p => p.OrderProducts).ToList());
            }
            IsSourcesLoaded = true;
            SourcesLoadedChanged(this, EventArgs.Empty);
            //throw new Exception("Тестовое исключение");
        });

        public string Name { get; } = "ООО «Ткани»";
        public bool IsSourcesLoaded { get; private set; }

        public event EventHandler SourcesLoadedChanged = delegate { };

        private readonly List<Manufacturer> manufacturers = new();
        private readonly List<Unit> units = new();
        private readonly List<Supplier> suppliers = new();
        private readonly List<Category> categories = new();

        public IReadOnlyList<IUnit> GetUnits() => units;
        public IReadOnlyList<IManufacturer> GetManufacturers() => manufacturers;
        public IReadOnlyList<ISupplier> GetSuppliers() => suppliers;
        public IReadOnlyList<ICategory> GetCategories() => categories;
    }
}
