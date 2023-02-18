using ShopModel.DTOs;
using ShopModel.Entities;

namespace ShopModel
{
    public partial class Shop
    {
        //public Authorization Authorization { get; } = new();

        //private static ProductDTO ToProductDTO(Product p) => new(p);

        public IEnumerable<ProductDTO> GetProducts()
        {
            //using var context = Context.Get();
            //return context.Products.Include(p => p.ProductUnitNavigation)
            //    .Include(p => p.ProductManufacturerNavigation)
            //    .Include(p => p.ProductSupplierNavigation)
            //    .Include(p => p.ProductCategoryNavigation).ToList().Select(ToProductDTO);

            var unit = new Unit { IdUnit = 1, UnitName = "шт." };

            Manufacturer[] manufacturers =
            {
                new Manufacturer{ IdManufacturer = 1, ManufacturerName = "БТК Текстиль" },
                new Manufacturer{ IdManufacturer = 2, ManufacturerName = "Империя ткани" },
                new Manufacturer{ IdManufacturer = 3, ManufacturerName = "Комильфо" },
                new Manufacturer{ IdManufacturer = 4, ManufacturerName = "Май Фабрик" }
            };

            Supplier[] suppliers =
            {
                new Supplier { IdSupplier = 1, SupplierName = "Раута" },
                new Supplier { IdSupplier = 2, SupplierName = "ООО Афо-Тек" },
                new Supplier { IdSupplier = 3, SupplierName = "ГК Петров" }
            };

            Category[] categories =
            {
                new Category { IdCategory = 1, CategoryName = "Постельные ткани" },
                new Category { IdCategory = 2, CategoryName = "Детские ткани" },
                new Category { IdCategory = 3, CategoryName = "Ткани для изделий" }
            };

            var lines = File.ReadAllLines("../../../../ShopModel/products.txt");

            foreach(string line in lines)
            {
                string[] props = line.Split('\t');
                yield return new ProductDTO(
                    new Product
                    {
                        ProductArticleNumber = props[0],
                        ProductName = props[1],
                        ProductUnitNavigation = unit,
                        ProductCost = decimal.Parse(props[3], System.Globalization.CultureInfo.InvariantCulture),
                        ProductManufacturerNavigation = manufacturers[int.Parse(props[4]) - 1],
                        ProductSupplierNavigation = suppliers[int.Parse(props[5]) - 1],
                        ProductCategoryNavigation = categories[int.Parse(props[6]) - 1],
                        ProductMaxDiscountAmount = int.Parse(props[7]),
                        ProductDiscountAmount = sbyte.Parse(props[8]),
                        ProductQuantityInStock = int.Parse(props[9]),
                        ProductDescription = props[10],
                        ProductPhoto = props[11].Trim() is "" ? null : props[11]
                    });
            }
        }

        public static bool Set<T>(ref T? storage, T? newValue)
        {
            if (Equals(storage, newValue))
                return false;
            storage = newValue;
            return true;
        }
    }
}
