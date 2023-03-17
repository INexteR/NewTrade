
using CommonNet6.Collection;
using Mapping;
using Model;
using ShopSQLite.Entities;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ShopSQLite
{
    public partial class Shop : IProductsSource
    {
        public event NotifyListChangedEventHandler<IProduct> ProductChanged = (_, _) => { };

        private void RoleVerifyAccess(Rights rights, [CallerMemberName] string methodName = "")
        {
            if (!RoleCheckAccess(rights))
                throw new MethodAccessException($"Доступ к методу \"{methodName}\" для роли {CurrentUser?.Role.Name ?? "null"} запрещён;");
        }
        public bool RoleCheckAccess(Rights rights)
        {
            return CurrentUser?.Role.Rights is Rights currentRights && (currentRights & rights) == rights;
        }

        public void Add(IProduct product)
        {
            RoleVerifyAccess(Rights.Adding);

            Product @new = product.Create<Product>();
            @new.Id = 0;

            var entry = catalog.Products.Add(@new);
            catalog.SaveChanges();

            products.Add(@new);
            ProductChanged(this, NotifyCollectionChangedAction<IProduct>.Add(@new));
        }

        public void Update(IProduct product)
        {
            Product? old = catalog.Products.Find(product.Id) ??
                throw new ArgumentException("Товара с таким Id нет.", nameof(product));
            var props = typeof(IProduct).GetProperties();
            for (int i = -1; ++i < 12;)
            {
                var prop = props[i];
                var oldValue = prop.GetValue(old);
                var newValue = prop.GetValue(product);
                if (!Equals(oldValue, newValue))
                    goto update;
            }
            return;
        update:
            catalog.Entry(old).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            Product @new = product.Create<Product>();
            var entry = catalog.Products.Update(@new);
            catalog.SaveChanges();

            ProductChanged(this, NotifyCollectionChangedAction<IProduct>.Replace(old, @new));
        }

        public void Remove(IProduct product)
        {
            Product? old = catalog.Products.Find(product.Id) ??
                throw new ArgumentException("Товара с таким Id нет.", nameof(product));
            if (old.OrderProducts.Count is 0)
            {
                catalog.Products.Remove(old);
                catalog.SaveChanges();

                ProductChanged(this, NotifyCollectionChangedAction<IProduct>.Remove(old));
            }
            else
            {
                throw new InvalidOperationException("Данный товар присутствует в заказе, его нельзя удалить");
            }
        }

        private readonly ObservableCollection<Product> products;
        public IEnumerable<IProduct> GetProducts() => products.Select(x => x);
    }
}
