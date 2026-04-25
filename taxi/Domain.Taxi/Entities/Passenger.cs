using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;
using System.Net;

namespace Domain.Taxi.Entities
{
    public class Passenger : Entity<Guid>
    {
        private readonly ICollection<Order> _orders = [];
        private readonly ICollection<Feedback> _feedbacks = [];

        public PassengerName Name { get; private set; }
        public decimal Rating { get; private set; }
        public IReadOnlyCollection<Order> Orders => _orders.ToList().AsReadOnly();
        public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.ToList().AsReadOnly();

        public Passenger(Guid id, PassengerName name) : base(id)
        {
            Name = name ?? throw new ArgumentNullValueException(nameof(name));
            Rating = 5.0m;
        }

        public Passenger(PassengerName name) : this(Guid.NewGuid(), name)
        {
        }

        public Order CreateOrder(Tariff tariff, Address startAddress, Address endAddress)
        {
            if (tariff == null) throw new ArgumentNullValueException(nameof(tariff));
            if (startAddress == null) throw new ArgumentNullValueException(nameof(startAddress));
            if (endAddress == null) throw new ArgumentNullValueException(nameof(endAddress));

            var order = new Order(this, tariff, startAddress, endAddress);
            _orders.Add(order);
            return order;
        }

        internal void UpdateRating(decimal newScore)
        {
            if (newScore < 1 || newScore > 5)
                throw new InvalidRatingValueException(newScore);

            var allScores = _feedbacks.Select(f => (decimal)f.Score).Concat([Rating]);
            Rating = allScores.Average();
        }

        internal void AddFeedback(Feedback feedback)
        {
            if (feedback == null) throw new ArgumentNullValueException(nameof(feedback));
            _feedbacks.Add(feedback);
            UpdateRating(feedback.Score);
        }
    }
}