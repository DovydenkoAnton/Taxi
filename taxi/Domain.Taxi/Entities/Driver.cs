using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;

namespace Domain.Taxi.Entities;

/// <summary>
/// Represents a driver who fulfills orders.
/// </summary>
public class Driver : Entity<Guid>
{
    private readonly List<Order> _orders = new();
    private readonly List<Feedback> _feedbacks = new();

    public DriverName Name { get; private set; }
    public decimal Rating { get; private set; }
    public Car Car { get; private set; }
    public Tariff Tariff { get; private set; }
    public bool IsAvailable { get; private set; } = true;
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();

    protected Driver() { }

    public Driver(Guid id, DriverName name, Car car, Tariff tariff) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Car = car ?? throw new ArgumentNullValueException(nameof(car));
        Tariff = tariff ?? throw new ArgumentNullValueException(nameof(tariff));
        Rating = 5.0m;
    }

    public Driver(DriverName name, Car car, Tariff tariff)
        : this(Guid.NewGuid(), name, car, tariff) { }

    /// <summary>
    /// Assigns driver to an order.
    /// </summary>
    public void AssignToOrder(Order order)
    {
        if (order == null) throw new ArgumentNullValueException(nameof(order));
        if (!IsAvailable) throw new InvalidOperationException("Driver is not available");
        if (order.Tariff != Tariff)
            throw new InvalidOperationException($"Driver's tariff {Tariff.Name} does not match order's tariff {order.Tariff.Name}");

        order.AssignDriver(this);
        _orders.Add(order);
        IsAvailable = false;
    }

    /// <summary>
    /// Completes the order and makes driver available again.
    /// </summary>
    public void CompleteOrder(Order order)
    {
        if (order == null) throw new ArgumentNullValueException(nameof(order));
        if (order.Driver != this)
            throw new InvalidOperationException($"Order {order.Id} is not assigned to this driver");

        order.Complete();
        IsAvailable = true;
    }

    /// <summary>
    /// Updates driver rating based on new feedback.
    /// </summary>
    public void UpdateRating(decimal newScore)
    {
        if (newScore < 1 || newScore > 5)
            throw new InvalidRatingValueException(newScore);

        var allScores = _feedbacks.Select(f => (decimal)f.Score).Concat(new[] { Rating });
        Rating = allScores.Average();
    }

    internal void AddFeedback(Feedback feedback)
    {
        if (feedback == null) throw new ArgumentNullValueException(nameof(feedback));
        _feedbacks.Add(feedback);
        UpdateRating(feedback.Score);
    }
}