using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Order[]? orders;
        public static IEnumerable<Order> GetOrders()
        {
            if (orders is null)
            {
                var lines = File.ReadAllLines(ordersDataFullName);
                orders = new Order[lines.Length];

                if (pickuppoints is null) GetPickuppoints();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] props = line.Split('\t');
                    int pickuppointId = int.Parse(props[3]);
                    int orderstatusId = int.Parse(props[6]);
                    orders[i] = new Order
                    {
                        Id = int.Parse(props[0]),
                        OrderDate = DateOnly.Parse(props[1], System.Globalization.CultureInfo.InvariantCulture),
                        DeliveryDate = DateOnly.Parse(props[2], System.Globalization.CultureInfo.InvariantCulture),
                        PickupPointId = pickuppointId,
                        PickupPoint = pickuppoints![pickuppointId - 1],
                        ClientName = props[4],
                        CodeToGet = int.Parse(props[5]),
                        OrderStatusId = orderstatusId,
                        OrderStatus = orderstatuses[orderstatusId - 1]
                    };
                }
            }
            return orders;
        }
    }
}
