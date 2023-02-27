
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

        public async Task LoadDataAsync() => await Task.Run(() =>
        {
            // Задержка для имитации долго запроса
            Thread.Sleep(5000);

            var db = CatalogContext.Get(сonnectionString);
            db.Database.EnsureCreated();
            manufacturers = new ReadOnlyCollection<IManufacturer>(db.Manufacturers.ToArray());
            GetProducts();

            throw new Exception("Тестовое исключение");
        });

        public string Name { get; } = "ООО «Ткани»";


        private  ReadOnlyCollection<IManufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> GetManufacturers()
            => manufacturers;
    }
}
