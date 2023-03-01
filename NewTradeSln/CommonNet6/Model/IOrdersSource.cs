using System;
using System.Collections.Generic;

namespace Model
{
    public interface IOrdersSource
    {
        event EventHandler OrdersChanged;
        IReadOnlyList<IOrder> GetOrders();
    }
}
