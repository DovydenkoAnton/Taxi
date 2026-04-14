using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the license plate.
/// </summary>
/// <param name="plate">The license plate of the car.</param>
public class LicensePlate(string plate) : ValueObject<string>(new LicensePlateValidator(), plate);