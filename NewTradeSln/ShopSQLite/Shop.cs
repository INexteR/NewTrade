
using CommonNet6.Collection;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Model;
using System.Collections.ObjectModel;

namespace ShopSQLite
{
    public partial class Shop : IShop
    {
        const string сonnectionString = "sqliteTest.db";

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

        public Task LoadDataAsync() => Task.Run(() =>
        {
            using var db = CatalogContext.Get(сonnectionString);
            db.Database.EnsureCreated();
            GetProducts();
            GetManufacturers();
            //почему-то не перехватывается ↓
            //throw new Exception("Тестовое исключение");
        });

        public string Name { get; } = "ООО «Ткани»";



        private readonly List<IManufacturer> manufacturers = new();

        public event NotifyListChangedEventHandler<IManufacturer> ManufacturerChanged = delegate { };

        public IReadOnlyCollection<IManufacturer> GetManufacturers()
        {
            lock (((ICollection)manufacturers).SyncRoot)
            {
                if (manufacturers.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                        manufacturers.AddRange(context.Manufacturers.ToArray());

                    ManufacturerChanged(this, NotifyListChangedEventArgs<IManufacturer>.Reset());
                }
            }
            return manufacturers;
        }
    }
}
