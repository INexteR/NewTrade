
using ShopModel.Entities;
using ShopModel.Testing;

namespace ShopModel
{
    public partial class Shop : IShop
    {       
        public string Name { get; } = "ООО «Ткани»";

        public IEnumerable<IProduct> GetProducts()
        {
            //using var context = ShopContext.Get();
            //return context.Products.Include(p => p.Unit)
            //    .Include(p => p.Manufacturer)
            //    .Include(p => p.Supplier)
            //    .Include(p => p.Category).ToList();
            return TestData.GetProducts();
        }

        public IEnumerable<IManufacturer> GetManufacturers()
        {
            return TestData.GetManufacturers();
        }
    }
}
