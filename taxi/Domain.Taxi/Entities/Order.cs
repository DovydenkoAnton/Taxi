using Domain.Taxi.Base;
using Domain.Taxi.Enums;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;

namespace Domain.Taxi.Entities
{
    public class Order : Entity<Guid>
    {
        private readonly ICollection<Feedback> _feedbacks = [];

        public Passenger Passenger { get; private set; } = default!;
        public Driver? Driver { get; private set; }
        public Tariff Tariff { get; private set; } = default!;
        public Address StartAddress { get; private set; } = default!;
        public Address EndAddress { get; private set; } = default!;
        public OrderStatus Status { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime? CompletedAt { get; private set; }
        public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks.ToList().AsReadOnly();

        protected Order()
        {
            CreatedAt = DateTime.UtcNow;
        }

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
            Price = 0;
        }

        public Order(Passenger passenger, Tariff tariff, Address startAddress, Address endAddress)
            : this(Guid.NewGuid(), passenger, tariff, startAddress, endAddress)
        {
        }

        public void AssignDriver(Driver driver)
        {
            if (driver == null) throw new ArgumentNullValueException(nameof(driver));
            if (Status != OrderStatus.Searching)
                throw new InvalidOrderStatusException(this, OrderStatus.Searching, Status);
            if (Driver != null)
                throw new DriverAlreadyAssignedException(this, Driver);

            Driver = driver;
            Status = OrderStatus.InProgress;
        }

        public void Complete()
        {
            if (Status != OrderStatus.InProgress)
                throw new InvalidOrderStatusException(this, OrderStatus.InProgress, Status);
            if (Driver == null)
                throw new OrderNotAssignedException(this);

            Status = OrderStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
                throw new InvalidOperationException("Cannot cancel completed order");

            Status = OrderStatus.Cancelled;
        }

        public Feedback AddFeedback(Passenger from, Driver to, int score, Comment comment)
        {
            if (from == null) throw new ArgumentNullValueException(nameof(from));
            if (to == null) throw new ArgumentNullValueException(nameof(to));
            if (score < 1 || score > 5) throw new InvalidRatingValueException(score);

            var feedback = new Feedback(this, from, to, score, comment);
            _feedbacks.Add(feedback);
            to.AddFeedback(feedback);

            return feedback;
        }

        public Feedback AddFeedback(Driver from, Passenger to, int score, Comment comment)
        {
            if (from == null) throw new ArgumentNullValueException(nameof(from));
            if (to == null) throw new ArgumentNullValueException(nameof(to));
            if (score < 1 || score > 5) throw new InvalidRatingValueException(score);

            var feedback = new Feedback(this, from, to, score, comment);
            _feedbacks.Add(feedback);
            to.AddFeedback(feedback);

            return feedback;
        }
    }
}