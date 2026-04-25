using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;
using System.Reflection;

namespace Domain.Taxi.Entities
{
    public class Car : Entity<Guid>
    {
        public Brand Brand { get; private set; }
        public Model Model { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public string Color { get; private set; }

        public Car(Guid id, Brand brand, Model model, LicensePlate licensePlate, string color) : base(id)
        {
            Brand = brand ?? throw new ArgumentNullValueException(nameof(brand));
            Model = model ?? throw new ArgumentNullValueException(nameof(model));
            LicensePlate = licensePlate ?? throw new ArgumentNullValueException(nameof(licensePlate));
            Color = color ?? throw new ArgumentNullValueException(nameof(color));
        }

        public Car(Brand brand, Model model, LicensePlate licensePlate, string color)
            : this(Guid.NewGuid(), brand, model, licensePlate, color)
        {
        }
    }
}