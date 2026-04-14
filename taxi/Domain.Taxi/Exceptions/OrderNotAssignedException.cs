using Domain.Taxi.Entities;

namespace Domain.Taxi.Exceptions;

public class OrderNotAssignedException(Order order)
    : InvalidOperationException($"Order {order.Id} has no assigned driver")
{
    public Order Order => order;
}