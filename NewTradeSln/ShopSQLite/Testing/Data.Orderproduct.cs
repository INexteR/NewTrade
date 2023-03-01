using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Orderproduct[]? orderproduct;
        public static IEnumerable<Orderproduct> GetOrderproduct()
        {
            if (orderproduct is null)
            {
                var lines = File.ReadAllLines(orderproductDataFullName);
                orderproduct = new Orderproduct[lines.Length];

                if (orders is null) GetOrders();
                if (products is null) GetProducts();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    int orderId = int.Parse(props[0]);
                    int productId = int.Parse(props[1]);
                    orderproduct[i] = new Orderproduct
                    {
                        OrderId = orderId,
                        //Order = orders![orderId - 1],
                        ProductId = productId,
                        //Product = products![productId - 1],
                        ProductCount = int.Parse(props[2])
                    };
                }
            }
            return orderproduct;
        }
    }
}
