
using CommonNet6.Collection;
using Microsoft.EntityFrameworkCore;
using Model;
using ShopSQLite.Entities;
using System.Collections;

namespace ShopSQLite
{
    public partial class Shop : IProductsSource
    {
        public event NotifyListChangedEventHandler<IProduct> ProductChanged = (_, _) => { };

        public void Add(IProduct product)
        {
            Product @new;
            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                context.Products.Add(product.Create());
                context.SaveChanges();
                @new = context.Products
                    .Include(p => p.Unit)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Supplier)
                    .Include(p => p.Category)
                    .First(p => p.Id == product.Id);
            }
            productsList.Add(@new);
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Add(@new, productsList.Count - 1));
        }

        public void Change(IProduct product)
        {
            Product @new;
            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                context.Products.Update(product.Create());
                context.SaveChanges();
                @new = context.Products
                    .Include(p => p.Unit)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.Supplier)
                    .Include(p => p.Category)
                    .First(p => p.Id == product.Id);
            }
            int index = productsList.FindIndex(pr => string.Equals(pr.Id, @new.Id));
            Product old = productsList[index];
            productsList[index] = @new;
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Replace(old, @new, index));
        }
        public void Remove(IProduct product)
        {
            int index = productsList.FindIndex(pr => string.Equals(pr.Id, product.Id));
            Product old = productsList[index];

            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                context.Products.Remove(old);
                context.SaveChanges();
            }
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Remove(old, index));
        }

        private readonly List<Product> productsList = new List<Product>();
        public IReadOnlyCollection<IProduct> GetProducts()
        {
            lock (((ICollection)productsList).SyncRoot)
            {
                if (productsList.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                        productsList.AddRange(context.Products.Include(p => p.Unit)
                            .Include(p => p.Manufacturer)
                            .Include(p => p.Supplier)
                            .Include(p => p.Category).ToArray());
                    ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Reset());
                }
            }
            return productsList.ToArray();
        }

        public void Refresh()
        {
            productsList.Clear();
            GetProducts();
        }
    }
}
