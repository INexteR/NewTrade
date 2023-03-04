using System;
using System.Collections.Generic;

namespace Model
{
    public interface IOrdersSource
    {
        IReadOnlyList<IOrder> GetOrders();
    }
}
