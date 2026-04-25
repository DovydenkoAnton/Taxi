using Domain.Taxi.Entities;

namespace Domain.Taxi.Exceptions
{
    public class DriverAlreadyAssignedException(Order order, Driver driver)
        : InvalidOperationException($"Driver {driver.Id} is already assigned to order {order.Id}")
    {
        public Order Order => order;
        public Driver Driver => driver;
    }
}