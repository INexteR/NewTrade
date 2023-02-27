using CommonNet6.Collection;
using System.Collections.Generic;

namespace Model
{
    public interface IProductsSource
    {
        event NotifyListChangedEventHandler<IProduct> ProductChanged;
        IList<IProduct> GetProducts();

        void Add(IProduct product);
        void Remove(IProduct product);
        void Change(string ArticleNumber, IProduct product);
        void Refresh();
    }
}
