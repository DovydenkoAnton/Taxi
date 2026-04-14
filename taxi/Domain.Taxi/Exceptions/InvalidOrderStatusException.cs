using Domain.Taxi.Entities;
using Domain.Taxi.Enums;

namespace Domain.Taxi.Exceptions;

public class InvalidOrderStatusException : InvalidOperationException
{
    public Order Order { get; }
    public OrderStatus ExpectedStatus { get; }
    public OrderStatus ActualStatus { get; }

    public InvalidOrderStatusException(Order order, OrderStatus expectedStatus, OrderStatus actualStatus)
        : base($"Order {order.Id} has status {actualStatus}, but expected {expectedStatus}")
    {
        Order = order;
        ExpectedStatus = expectedStatus;
        ActualStatus = actualStatus;
    }
}