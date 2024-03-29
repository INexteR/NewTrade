﻿using Mapping;
using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static OrderProduct[]? orderproduct;
        public static IEnumerable<OrderProduct> GetOrderproduct()
        {
            return orderproduct ??= orderproductText.ParseToArray<OrderProduct>(nameof(OrderProduct.OrderId),
                                                                                nameof(OrderProduct.ProductId),
                                                                                nameof(OrderProduct.ProductCount));
           
        }
        private const string orderproductText = @"1	1	5
1	20	7
2	3	5
2	20	9
3	10	8
3	11	4
4	12	6
4	19	1
5	23	2
6	18	1
7	6	2
7	16	5
8	17	3
8	24	1
9	8	3
9	22	2
10	7	7
10	15	6";
    }
}
