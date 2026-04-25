using Domain.Taxi.Entities;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;

namespace DomainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TAXI SERVICE DEMO\n");

            var passengerName = new PassengerName("Ivan Petrov");
            var driverName = new DriverName("Alex Smirnov");
            var brand = new Brand("Tesla");
            var model = new Model("Model 3");
            var plate = new LicensePlate("A123BC");
            var tariffName = new TariffName("Econom");
            var startAddr = new Address("Moscow, Tverskaya 1");
            var endAddr = new Address("Moscow, Arbat 15");
            var comment = new Comment("Great ride!");

            var car = new Car(brand, model, plate, "Red");
            var tariff = new Tariff(tariffName, "Budget rides");
            var passenger = new Passenger(passengerName);
            var driver = new Driver(driverName, car, tariff);

            Console.WriteLine($"Passenger: {passenger.Name.Value}");
            Console.WriteLine($"Driver: {driver.Name.Value}");
            Console.WriteLine($"Car: {car.Brand.Value} {car.Model.Value}\n");

            var order = passenger.CreateOrder(tariff, startAddr, endAddr);
            Console.WriteLine($"Order created: {order.Id}");
            Console.WriteLine($"Status: {order.Status}");

            driver.AssignToOrder(order);
            Console.WriteLine($"After assign: {order.Status}");
            Console.WriteLine($"Driver: {order.Driver?.Name.Value}");

            driver.CompleteOrder(order);
            Console.WriteLine($"After complete: {order.Status}");

            var feedback = order.AddFeedback(passenger, driver, 5, comment);
            Console.WriteLine($"Feedback: {feedback.Score}/5 - {feedback.Comment.Value}");
            Console.WriteLine($"Driver rating: {driver.Rating:F1}");

            Console.WriteLine("\nDEMO COMPLETED");
        }
    }
}