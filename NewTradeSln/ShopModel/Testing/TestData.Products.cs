using Common;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    public static partial class TestData
    {
        private const string testProductsData = @"products.txt";
        private static readonly string productsDataFullName = Path.Combine(folderFullName, testProductsData);

        public static IEnumerable<Product> GetProducts()
        {
            var unit = new Unit { Id = 1, Name = "шт." };

            Manufacturer[] manufacturers =
            {
                new Manufacturer{ Id = 1, Name = "БТК Текстиль" },
                new Manufacturer{ Id = 2, Name = "Империя ткани" },
                new Manufacturer{ Id = 3, Name = "Комильфо" },
                new Manufacturer{ Id = 4, Name = "Май Фабрик" }
            };

            Supplier[] suppliers =
            {
                new Supplier { Id = 1, Name = "Раута" },
                new Supplier { Id = 2, Name = "ООО Афо-Тек" },
                new Supplier { Id = 3, Name = "ГК Петров" }
            };

            Category[] categories =
            {
                new Category { Id = 1, Name = "Постельные ткани" },
                new Category { Id = 2, Name = "Детские ткани" },
                new Category { Id = 3, Name = "Ткани для изделий" }
            };

            var lines = File.ReadAllLines(productsDataFullName);

            foreach (var line in lines)
            {
                string[] props = line.Split('\t');
                yield return new Product
                {
                    ArticleNumber = props[0],
                    Name = props[1],
                    Unit = unit,
                    Cost = decimal.Parse(props[3], System.Globalization.CultureInfo.InvariantCulture),
                    Manufacturer = manufacturers[int.Parse(props[4]) - 1],
                    Supplier = suppliers[int.Parse(props[5]) - 1],
                    Category = categories[int.Parse(props[6]) - 1],
                    MaxDiscountAmount = int.Parse(props[7]),
                    DiscountAmount = sbyte.Parse(props[8]),
                    QuantityInStock = int.Parse(props[9]),
                    Description = props[10],
                    Path = props[11].Trim() is "" ? null : props[11]
                };
            }
        }
    }
}
