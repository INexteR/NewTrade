using Microsoft.EntityFrameworkCore;
using Model;
using ShopSQLite.Entities;
using System.Collections.ObjectModel;

namespace ShopSQLite
{
    public partial class Shop : IShop
    {
        const string сonnectionString = "sqliteTest.db";

        private readonly CatalogContext catalog;

        public Shop(bool recreate = false)
        {
            if (recreate)
            {
                if (File.Exists(сonnectionString))
                    File.Delete(сonnectionString);
            }
            catalog = CatalogContext.Get(сonnectionString);
            catalog.Database.EnsureCreated();

            manufacturers = catalog.Manufacturers.Local.ToObservableCollection();
            units = catalog.Units.Local.ToObservableCollection();
            suppliers = catalog.Suppliers.Local.ToObservableCollection();
            categories = catalog.Categories.Local.ToObservableCollection();
            products = catalog.Products.Local.ToObservableCollection();
        }

        public Task LoadDataAsync() => Task.Run(() =>
            {
                catalog.Units.Load();
                catalog.Manufacturers.Load();
                catalog.Suppliers.Load();
                catalog.Categories.Load();
                catalog.Products.Load();

                IsSourcesLoaded = true;
                SourcesLoadedChanged(this, EventArgs.Empty);
                //throw new Exception("Тестовое исключение");
            });

        public string Name { get; } = "ООО «Ткани»";
        public bool IsSourcesLoaded { get; private set; }

        public event EventHandler SourcesLoadedChanged = delegate { };

        private readonly ObservableCollection<Manufacturer> manufacturers;
        private readonly ObservableCollection<Unit> units;
        private readonly ObservableCollection<Supplier> suppliers;
        private readonly ObservableCollection<Category> categories;

        public IEnumerable<IUnit> GetUnits() => units.Select(x => x);
        public IEnumerable<IManufacturer> GetManufacturers() => manufacturers.Select(x => x);
        public IEnumerable<ISupplier> GetSuppliers() => suppliers.Select(x => x);
        public IEnumerable<ICategory> GetCategories() => categories.Select(x => x);
    }
}
