
using CommonNet6.Collection;
using Mapping;
using Model;
using ShopSQLite.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopSQLite
{
    public partial class Shop : IProductsSource
    {
        public event NotifyListChangedEventHandler<IProduct> ProductChanged = (_, _) => { };

        public void Add(IProduct product)
        {
            Product @new = product.Create<Product>();
            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                var entry = context.Products.Add(@new);
                context.SaveChanges();

                context.Units.AttachRange(units);
                context.Manufacturers.AttachRange(manufacturers);
                context.Suppliers.AttachRange(suppliers);
                context.Categories.AttachRange(categories);

                entry.Reference(p => p.Unit).Load();
                entry.Reference(p => p.Manufacturer).Load();
                entry.Reference(p => p.Supplier).Load();
                entry.Reference(p => p.Category).Load();
            }
            products.Add(@new);
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Add(@new, products.Count - 1));
        }

        public void Change(IProduct product)
        {
            Product @new = product.Create<Product>();
            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                var entry = context.Products.Update(@new);
                context.SaveChanges();

                context.Units.AttachRange(units);
                context.Manufacturers.AttachRange(manufacturers);
                context.Suppliers.AttachRange(suppliers);
                context.Categories.AttachRange(categories);

                entry.Reference(p => p.Unit).Load();
                entry.Reference(p => p.Manufacturer).Load();
                entry.Reference(p => p.Supplier).Load();
                entry.Reference(p => p.Category).Load();
            }
            int index = products.FindIndex(pr => pr.Id == @new.Id);
            Product old = products[index];
            products[index] = @new;
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Replace(old, @new, index));
        }
        public void Remove(IProduct product)
        {
            int index = products.FindIndex(pr => pr.Id == product.Id);
            Product old = products[index];

            if (old.OrderProducts.Count is 0)
            {
                using (CatalogContext context = CatalogContext.Get(сonnectionString))
                {
                    context.Products.Remove(old);
                    context.SaveChanges();
                }
                ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Remove(old, index));
            }
            else
            {
                throw new InvalidOperationException("Данный товар присутствует в заказе, его нельзя удалить");
            }
        }

        private readonly List<Product> products = new();
        public IReadOnlyCollection<IProduct> GetProducts() => products;
    }
}
