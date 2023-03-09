namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Order[]? orders;
        public static IEnumerable<Order> GetOrders()
        {          
            return orders ??= ordersText.ParseToArray<Order>(nameof(Order.Id),
                                                      nameof(Order.OrderDate),
                                                      nameof(Order.DeliveryDate),
                                                      nameof(Order.PickupPointId),
                                                      nameof(Order.ClientName),
                                                      nameof(Order.CodeToGet),
                                                      nameof(Order.OrderStatusId));
        }

        private const string ordersText = @"
1	2022-05-10	2022-05-16	27	Маслов Максим Иванович	811	1
2	2022-05-11	2022-05-17	5		812	1
3	2022-05-12	2022-05-18	29		813	1
4	2022-05-13	2022-05-19	10		814	1
5	2022-05-14	2022-05-20	31	Симонов Михаил Тимурович	815	1
6	2022-05-15	2022-05-21	32		816	1
7	2022-05-16	2022-05-22	20		817	1
8	2022-05-17	2022-05-23	34	Павлова Ксения Михайловна	818	2
9	2022-05-18	2022-05-24	35	Трифонов Григорий Юрьевич	819	1
10	2022-05-19	2022-05-25	36		820	2";
    }
}
