using System.Collections.Generic;

namespace Model
{
    public interface IProductsSource
    {
        IList<IProduct> GetProducts();
    }
}
