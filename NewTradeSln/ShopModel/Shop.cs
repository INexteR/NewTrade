using Interfaces;
using ShopModel.Entities;
using ShopModel.Testing;
using System.Collections.ObjectModel;

namespace ShopModel
{
    public partial class Shop : IShop
    {
        public Shop()
        {
            // Создание оболочки только для чтения
            manufacturers = new ReadOnlyCollection<Manufacturer>(TestData.GetManufacturers().ToArray());
        }


        public string Name { get; } = "ООО «Ткани»";

        public IEnumerable<IProduct> GetProducts()
        {
            return TestData.GetProducts();
        }

    }
}
