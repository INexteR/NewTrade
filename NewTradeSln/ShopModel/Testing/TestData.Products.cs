using ShopModel.Entities;

namespace ShopModel.Testing
{
    internal static partial class TestData
    {
        private static readonly Supplier[] suppliers =
            {
                new Supplier { Id = 1, Name = "Раута" },
                new Supplier { Id = 2, Name = "ООО Афо-Тек" },
                new Supplier { Id = 3, Name = "ГК Петров" }
            };


        private static readonly Category[] categories =
            {
                new Category { Id = 1, Name = "Постельные ткани" },
                new Category { Id = 2, Name = "Детские ткани" },
                new Category { Id = 3, Name = "Ткани для изделий" }
            };
        private static Unit unit = new Unit { Id = 1, Name = "шт." };

        private static Product[]? products;
        public static IEnumerable<Product> GetProducts()
        {
            if (products == null)
            {

                var lines = File.ReadAllLines(productsDataFullName);
                products = new Product[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    string[] props = line.Split('\t');
                    products[i] = new Product
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
                        Path = string.IsNullOrWhiteSpace(props[11]) ? null : props[11].Trim()
                    };
                    //throw new NotImplementedException();
                }

            }
            foreach (var product in products)
            {
                yield return product;
            }
        }
    }
}
