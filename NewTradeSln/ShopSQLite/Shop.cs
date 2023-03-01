
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
            _ = GetProducts();
            _ = GetManufacturers();
            _ = GetOrders();
            //почему-то не перехватывается ↓
            //throw new Exception("Тестовое исключение");
        });

        public string Name { get; } = "ООО «Ткани»";



        private readonly List<IManufacturer> manufacturers = new();

        public event EventHandler ManufacturersChanged = delegate { };

        public IReadOnlyList<IManufacturer> GetManufacturers()
        {
            lock (((ICollection)manufacturers).SyncRoot)
            {
                if (manufacturers.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                        manufacturers.AddRange(context.Manufacturers.ToArray());

                    ManufacturersChanged(this, EventArgs.Empty);
                }
            }
            return manufacturers;
        }



        private readonly List<IOrder> orders = new();

        public event EventHandler OrdersChanged = delegate { };

        public IReadOnlyList<IOrder> GetOrders()
        {
            lock (((ICollection)manufacturers).SyncRoot)
            {
                if (orders.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                        orders.AddRange(context.Orders.ToArray());

                    OrdersChanged(this, EventArgs.Empty);
                }
            }
            return orders;
        }
    }
}
