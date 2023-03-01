using System;

namespace Model
{
    public interface IOrder
    {
        int Id { get; }
        DateOnly OrderDate { get; }
        DateOnly DeliveryDate { get; }
        string? ClientName { get; }
        int CodeToGet { get; }

        IOrderStatus OrderStatus { get; }
        IPickupPoint PickupPoint { get; }
    }
}
