using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;
using System.Xml.Linq;

namespace Domain.Taxi.Entities;

/// <summary>
/// Represents feedback left by one user for another.
/// </summary>
public class Feedback : Entity<Guid>
{
    public Order Order { get; private set; }
    public Guid FromId { get; private set; }  // Who left feedback
    public Guid ToId { get; private set; }    // Who received feedback
    public int Score { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime CreatedAt { get; }

    protected Feedback() { }

    public Feedback(
        Guid id,
        Order order,
        object from,
        object to,
        int score,
        Comment comment) : base(id)
    {
        Order = order ?? throw new ArgumentNullValueException(nameof(order));
        Score = score;
        Comment = comment ?? throw new ArgumentNullValueException(nameof(comment));
        CreatedAt = DateTime.UtcNow;

        // Store IDs of sender and receiver
        if (from is Passenger passenger)
            FromId = passenger.Id;
        else if (from is Driver driver)
            FromId = driver.Id;
        else
            throw new ArgumentException("From must be Passenger or Driver");

        if (to is Passenger pass)
            ToId = pass.Id;
        else if (to is Driver drv)
            ToId = drv.Id;
        else
            throw new ArgumentException("To must be Passenger or Driver");
    }

    public Feedback(Order order, object from, object to, int score, Comment comment)
        : this(Guid.NewGuid(), order, from, to, score, comment) { }
}