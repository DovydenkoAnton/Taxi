using Domain.Taxi.Base;
using Domain.Taxi.Enums;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;
using System.Net;
using System.Xml.Linq;

namespace Domain.Taxi.Entities;

/// <summary>
/// Represents a ride order.
/// </summary>
public class Order : Entity<Guid>
{
    private readonly List<Feedback> _feedbacks = new();

    public Passenger Passenger { get; private set; }
    public Driver? Driver { get; private set; }
    public Tariff Tariff { get; private set; }
    public Address StartAddress { get; private set; }
    public Address EndAddress { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();

    protected Order() { }

    public Order(
        Guid id,
        Passenger passenger,
        Tariff tariff,
        Address startAddress,
        Address endAddress) : base(id)
    {
        Passenger = passenger ?? throw new ArgumentNullValueException(nameof(passenger));
        Tariff = tariff ?? throw new ArgumentNullValueException(nameof(tariff));
        StartAddress = startAddress ?? throw new ArgumentNullValueException(nameof(startAddress));
        EndAddress = endAddress ?? throw new ArgumentNullValueException(nameof(endAddress));

        Status = OrderStatus.Searching;
        CreatedAt = DateTime.UtcNow;
        Price = 0; // Will be calculated when completed
    }

    public Order(Passenger passenger, Tariff tariff, Address startAddress, Address endAddress)
        : this(Guid.NewGuid(), passenger, tariff, startAddress, endAddress) { }

    /// <summary>
    /// Assigns a driver to this order.
    /// </summary>
    internal void AssignDriver(Driver driver)
    {
        if (driver == null) throw new ArgumentNullValueException(nameof(driver));
        if (Status != OrderStatus.Searching)
            throw new InvalidOrderStatusException(this, OrderStatus.Searching, Status);
        if (Driver != null)
            throw new DriverAlreadyAssignedException(this, Driver);

        Driver = driver;
        Status = OrderStatus.InProgress;
    }

    /// <summary>
    /// Completes the order and calculates price.
    /// </summary>
    internal void Complete()
    {
        if (Status != OrderStatus.InProgress)
            throw new InvalidOrderStatusException(this, OrderStatus.InProgress, Status);
        if (Driver == null)
            throw new OrderNotAssignedException(this);

        Status = OrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;

        // Здесь можно добавить логику расчёта цены
        // Price = CalculatePrice();
    }

    /// <summary>
    /// Cancels the order.
    /// </summary>
    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Cannot cancel completed order");

        Status = OrderStatus.Cancelled;
    }

    /// <summary>
    /// Adds feedback for this order.
    /// </summary>
    public Feedback AddFeedback(Passenger from, Driver to, int score, Comment comment)
    {
        if (from == null) throw new ArgumentNullValueException(nameof(from));
        if (to == null) throw new ArgumentNullValueException(nameof(to));
        if (score < 1 || score > 5) throw new InvalidRatingValueException(score);

        var feedback = new Feedback(this, from, to, score, comment);
        _feedbacks.Add(feedback);

        // Обновляем рейтинг получателя (водителя)
        to.AddFeedback(feedback);

        return feedback;
    }

    /// <summary>
    /// Adds feedback from driver to passenger.
    /// </summary>
    public Feedback AddFeedback(Driver from, Passenger to, int score, Comment comment)
    {
        if (from == null) throw new ArgumentNullValueException(nameof(from));
        if (to == null) throw new ArgumentNullValueException(nameof(to));
        if (score < 1 || score > 5) throw new InvalidRatingValueException(score);

        var feedback = new Feedback(this, from, to, score, comment);
        _feedbacks.Add(feedback);

        // Обновляем рейтинг получателя (пассажира)
        to.AddFeedback(feedback);

        return feedback;
    }
}