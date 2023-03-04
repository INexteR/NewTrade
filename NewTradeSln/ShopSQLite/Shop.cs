using System.Collections;
using ShopSQLite.Entities;
using Model;
using Microsoft.EntityFrameworkCore;

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
            _ = GetUnits();
            _ = GetManufacturers();
            _ = GetSuppliers();
            _ = GetCategories();
            _ = GetProducts();
            _ = GetOrders();
            //почему-то не перехватывается ↓
            //throw new Exception("Тестовое исключение");
        });

        public string Name { get; } = "ООО «Ткани»";



        private readonly List<Manufacturer> manufacturersList = new();

        public event EventHandler ManufacturersChanged = delegate { };

        public IReadOnlyList<IManufacturer> GetManufacturers()
        {
            lock (((ICollection)manufacturersList).SyncRoot)
            {
                if (manufacturersList.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                        manufacturersList.AddRange(context.Manufacturers.ToArray());

                    ManufacturersChanged(this, EventArgs.Empty);
                }
            }
            return manufacturersList;
        }



        private readonly List<Order> ordersList = new();

        public IReadOnlyList<IOrder> GetOrders()
        {
            lock (((ICollection)manufacturersList).SyncRoot)
            {
                if (ordersList.Count == 0)
                {
                    using var context = CatalogContext.Get(сonnectionString);
                    context.Products.AttachRange(productsList);
                    ordersList.AddRange(context.Orders.ToArray());
                    context.Products.Include(p => p.Orderproducts).Load();
                }
            }
            return ordersList;
        }



        private readonly List<Unit> unitsList = new();

        public IReadOnlyList<IUnit> GetUnits()
        {
            lock (((ICollection)unitsList).SyncRoot)
            {
                if (unitsList.Count == 0)
                {
                    using var context = CatalogContext.Get(сonnectionString);
                    unitsList.AddRange(context.Units.ToArray());
                }
            }
            return unitsList;
        }

        private readonly List<Supplier> suppliersList = new();

        public IReadOnlyList<ISupplier> GetSuppliers()
        {
            lock (((ICollection)suppliersList).SyncRoot)
            {
                if (suppliersList.Count == 0)
                {
                    using var context = CatalogContext.Get(сonnectionString);
                    suppliersList.AddRange(context.Suppliers.ToArray());
                }
            }
            return suppliersList;
        }

        private readonly List<Category> categoriesList = new();

        public IReadOnlyList<ICategory> GetCategories()
        {
            lock (((ICollection)categoriesList).SyncRoot)
            {
                if (categoriesList.Count == 0)
                {
                    using var context = CatalogContext.Get(сonnectionString);
                    categoriesList.AddRange(context.Categories.ToArray());
                }
            }
            return categoriesList;
        }
    }
}
