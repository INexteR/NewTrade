using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Product[]? products;
        public static IEnumerable<Product> GetProducts()
        {
            if (products is null)
            {
                var lines = File.ReadAllLines(productsDataFullName);
                products = new Product[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    var manufacturerId = int.Parse(props[4]);
                    products[i] = new Product
                    {
                        Id = int.Parse(props[0]),
                        Name = props[1],
                        UnitId = unit.Id,
                        Cost = decimal.Parse(props[3], System.Globalization.CultureInfo.InvariantCulture),
                        ManufacturerId = manufacturerId,
                        SupplierId = int.Parse(props[5]),
                        CategoryId = int.Parse(props[6]),
                        MaxDiscountAmount = int.Parse(props[7]),
                        DiscountAmount = sbyte.Parse(props[8]),
                        QuantityInStock = int.Parse(props[9]),
                        Description = props[10],
                        Path = props[11].Trim() is "" ? "/Resources/picture.png" : props[11]
                    };
                }
            }
            return products;
        }
    }
}
