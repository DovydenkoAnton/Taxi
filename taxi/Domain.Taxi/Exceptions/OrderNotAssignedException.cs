using Domain.Taxi.Entities;

namespace Domain.Taxi.Exceptions;

public class OrderNotAssignedException : InvalidOperationException
{
    public Order Order { get; }

    public OrderNotAssignedException(Order order)
        : base($"Order {order.Id} has no assigned driver")
    {
        Order = order;
    }
}