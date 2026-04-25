using Domain.Taxi.Entities;

namespace Domain.Taxi.Exceptions
{
    public class AnotherDriverAssignOrderException(Order order, Driver driver)
        : InvalidOperationException($"Driver {driver.Id} cannot be assigned to order {order.Id}")
    {
        public Order Order => order;
        public Driver Driver => driver;
    }
}