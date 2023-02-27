using Model;
using ShopSQLite.Entities;

namespace ShopSQLite
{
    internal static class ShopHelper
    {
        public static Product Create(this IProduct product)
        {
            return new()
            {
                ArticleNumber = product.ArticleNumber,
                CategoryId = product.CategoryId,
                Cost = product.Cost,
                Description = product.Description,
                DiscountAmount = product.DiscountAmount,
                ManufacturerId = product.ManufacturerId,
                MaxDiscountAmount = product.MaxDiscountAmount,
                Name = product.Name,
                Path = product.Path,
                QuantityInStock = product.QuantityInStock,
                SupplierId = product.SupplierId,
                UnitId = product.UnitId
            };
        }
    }
}
