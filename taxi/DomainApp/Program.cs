using Domain.Taxi.Entities;
using Domain.Taxi.Enums;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;

namespace DomainApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TAXI DOMAIN - FULL METHOD TEST ===\n");

            // 1. Value Objects
            Console.WriteLine("1. Value Objects Creation:");
            var passengerName = new PassengerName("Ivan Petrov");
            var driverName = new DriverName("Alex Smirnov");
            var brand = new Brand("Tesla");
            var model = new Model("Model 3");
            var licensePlate = new LicensePlate("A123BC");
            var tariffName = new TariffName("Econom");
            var startAddress = new Address("Moscow, Tverskaya 1");
            var endAddress = new Address("Moscow, Arbat 15");
            var comment = new Comment("Great ride!");
            Console.WriteLine($"   {passengerName.Value}, {driverName.Value}, {brand.Value}, {model.Value}\n");

            // 2. Entities
            Console.WriteLine("2. Entities Creation:");
            var car = new Car(brand, model, licensePlate, "Red");
            var tariff = new Tariff(tariffName, "Budget rides");
            var passenger = new Passenger(passengerName);
            var driver = new Driver(driverName, car, tariff);
            Console.WriteLine($"   Car: {car.Brand.Value} {car.Model.Value}");
            Console.WriteLine($"   Passenger: {passenger.Name.Value}, Driver: {driver.Name.Value}\n");

            // 3. Create Order
            Console.WriteLine("3. Create Order:");
            var order = passenger.CreateOrder(tariff, startAddress, endAddress);
            Console.WriteLine($"   Status: {order.Status} (Expected: Searching)\n");

            // 4. Assign Driver
            Console.WriteLine("4. Assign Driver:");
            driver.AssignToOrder(order);
            Console.WriteLine($"   Status: {order.Status} (Expected: InProgress)");
            Console.WriteLine($"   Driver: {order.Driver?.Name.Value}\n");

            // 5. Complete Order
            Console.WriteLine("5. Complete Order:");
            driver.CompleteOrder(order);
            Console.WriteLine($"   Status: {order.Status} (Expected: Completed)");
            Console.WriteLine($"   CompletedAt: {order.CompletedAt}\n");

            // 6. Cancel Order
            Console.WriteLine("6. Cancel Order:");
            var orderToCancel = passenger.CreateOrder(tariff, startAddress, endAddress);
            orderToCancel.Cancel();
            Console.WriteLine($"   Status after cancel: {orderToCancel.Status} (Expected: Cancelled)\n");

            // 7. Add Feedback (Passenger -> Driver)
            Console.WriteLine("7. Add Feedback (Passenger -> Driver):");
            var feedback1 = order.AddFeedback(passenger, driver, 5, comment);
            Console.WriteLine($"   Score: {feedback1.Score}/5, Driver Rating: {driver.Rating:F1}\n");

            // 8. Add Feedback (Driver -> Passenger)
            Console.WriteLine("8. Add Feedback (Driver -> Passenger):");
            var feedback2 = order.AddFeedback(driver, passenger, 4, new Comment("Good passenger"));
            Console.WriteLine($"   Score: {feedback2.Score}/5, Passenger Rating: {passenger.Rating:F1}\n");

            // 9. Exceptions
            Console.WriteLine("9. Exception Handling:");

            var comfortTariff = new Tariff(new TariffName("Comfort"), "Comfort");
            var wrongOrder = passenger.CreateOrder(comfortTariff, startAddress, endAddress);
            try { driver.AssignToOrder(wrongOrder); }
            catch (InvalidOperationException ex) { Console.WriteLine($"   Wrong tariff: {ex.Message}"); }

            try { driver.CompleteOrder(order); }
            catch (InvalidOrderStatusException ex) { Console.WriteLine($"   Double complete: {ex.Message}"); }

            try { order.AddFeedback(passenger, driver, 6, comment); }
            catch (InvalidRatingValueException ex) { Console.WriteLine($"   Invalid rating (6): {ex.Message}"); }

            try { order.AddFeedback(passenger, driver, 0, comment); }
            catch (InvalidRatingValueException ex) { Console.WriteLine($"   Invalid rating (0): {ex.Message}"); }
            Console.WriteLine();

            // 10. Collections
            Console.WriteLine("10. Feedbacks Collection:");
            Console.WriteLine($"   Order feedbacks count: {order.Feedbacks.Count} (Expected: 2)");
            Console.WriteLine($"   Driver feedbacks count: {driver.Feedbacks.Count}");
            Console.WriteLine($"   Passenger feedbacks count: {passenger.Feedbacks.Count}\n");

            // 11. Entity Equality
            Console.WriteLine("11. Entity Equality:");
            var passenger2 = new Passenger(new PassengerName("John"));
            Console.WriteLine($"   passenger == same: {passenger == passenger}");
            Console.WriteLine($"   passenger == different: {passenger == passenger2}\n");

            // 12. Value Object Equality
            Console.WriteLine("12. Value Object Equality:");
            var nameA = new PassengerName("Ivan");
            var nameB = new PassengerName("Ivan");
            var nameC = new PassengerName("Petr");
            Console.WriteLine($"   equal names: {nameA == nameB}");
            Console.WriteLine($"   different names: {nameA == nameC}\n");

            // 13. ToString
            Console.WriteLine("13. ToString Methods:");
            Console.WriteLine($"   passengerName: {passengerName}");
            Console.WriteLine($"   tariff: {tariff}");
            Console.WriteLine($"   order: {order}\n");

            // 14. Driver Availability
            Console.WriteLine("14. Driver Availability:");
            var newOrder = passenger.CreateOrder(tariff, startAddress, endAddress);
            Console.WriteLine($"   Before assign: {driver.IsAvailable}");
            driver.AssignToOrder(newOrder);
            Console.WriteLine($"   After assign: {driver.IsAvailable}");
            driver.CompleteOrder(newOrder);
            Console.WriteLine($"   After complete: {driver.IsAvailable}\n");

            Console.WriteLine("=== ALL TESTS COMPLETED ===");
        }
    }
}