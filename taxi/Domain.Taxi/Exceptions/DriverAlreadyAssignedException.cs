using Domain.Taxi.Entities;

namespace Domain.Taxi.Exceptions;

public class DriverAlreadyAssignedException : InvalidOperationException
{
    public Order Order { get; }
    public Driver Driver { get; }

    public DriverAlreadyAssignedException(Order order, Driver driver)
        : base($"Driver {driver.Id} is already assigned to order {order.Id}")
    {
        Order = order;
        Driver = driver;
    }
}