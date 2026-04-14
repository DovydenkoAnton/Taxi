using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;
using System.Net;

namespace Domain.Taxi.Entities;

/// <summary>
/// Represents a passenger who orders rides.
/// </summary>
public class Passenger : Entity<Guid>
{
    private readonly List<Order> _orders = new();
    private readonly List<Feedback> _feedbacks = new();

    public PassengerName Name { get; private set; }
    public decimal Rating { get; private set; }
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.AsReadOnly();

    protected Passenger() { }

    public Passenger(Guid id, PassengerName name) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Rating = 5.0m; // Начальный рейтинг
    }

    public Passenger(PassengerName name) : this(Guid.NewGuid(), name) { }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    public Order CreateOrder(Tariff tariff, Address startAddress, Address endAddress)
    {
        if (tariff == null) throw new ArgumentNullValueException(nameof(tariff));
        if (startAddress == null) throw new ArgumentNullValueException(nameof(startAddress));
        if (endAddress == null) throw new ArgumentNullValueException(nameof(endAddress));

        var order = new Order(this, tariff, startAddress, endAddress);
        _orders.Add(order);
        return order;
    }

    /// <summary>
    /// Updates passenger rating based on new feedback.
    /// </summary>
    public void UpdateRating(decimal newScore)
    {
        if (newScore < 1 || newScore > 5)
            throw new InvalidRatingValueException(newScore);

        // Средний рейтинг по всем отзывам
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