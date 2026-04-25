using Domain.Taxi.Entities;
using Domain.Taxi.Enums;

namespace Domain.Taxi.Exceptions
{
    public class InvalidOrderStatusException(Order order, OrderStatus expectedStatus, OrderStatus actualStatus)
        : InvalidOperationException($"Order {order.Id} has status {actualStatus}, but expected {expectedStatus}")
    {
        public Order Order => order;
        public OrderStatus ExpectedStatus => expectedStatus;
        public OrderStatus ActualStatus => actualStatus;
    }
}