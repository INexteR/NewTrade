
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
                context.Products.Add(product.Create()); //а зачем копию? нельзя сам product?
                context.SaveChanges();
                @new = context.Products.First(p => p.Id == product.Id);
                var entry = context.Entry(@new);
                entry.Reference(p => p.Unit).Load();
                entry.Reference(p => p.Manufacturer).Load();
                entry.Reference(p => p.Supplier).Load();
                entry.Reference(p => p.Category).Load();
            }
            productsList.Add(@new);
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Add(@new, productsList.Count - 1));
        }

        public void Change(IProduct product)
        {
            Product @new;
            using (CatalogContext context = CatalogContext.Get(сonnectionString))
            {
                context.Products.Update(product.Create()); //и снова зачем копию
                context.SaveChanges();
                @new = context.Products.First(p => p.Id == product.Id);
                var entry = context.Entry(@new);
                entry.Reference(p => p.Unit).Load();
                entry.Reference(p => p.Manufacturer).Load();
                entry.Reference(p => p.Supplier).Load();
                entry.Reference(p => p.Category).Load();
            }
            int index = productsList.FindIndex(pr => pr.Id == @new.Id);
            Product old = productsList[index];
            productsList[index] = @new;
            ProductChanged(this, NotifyListChangedEventArgs<IProduct>.Replace(old, @new, index));
        }
        public void Remove(IProduct product)
        {
            int index = productsList.FindIndex(pr => pr.Id == product.Id);
            Product old = productsList[index];

            if (old.Orderproducts.Count is 0)
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

        private readonly List<Product> productsList = new();
        public IReadOnlyCollection<IProduct> GetProducts()
        {
            lock (((ICollection)productsList).SyncRoot)
            {
                if (productsList.Count == 0)
                {
                    using (var context = CatalogContext.Get(сonnectionString))
                    {
                        context.Units.AttachRange(unitsList);
                        context.Manufacturers.AttachRange(manufacturersList);
                        context.Suppliers.AttachRange(suppliersList);
                        context.Categories.AttachRange(categoriesList);
                        productsList.AddRange(context.Products.Include(p => p.Unit)
                            .Include(p => p.Manufacturer)
                            .Include(p => p.Supplier)
                            .Include(p => p.Category).ToArray());
                    }
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
