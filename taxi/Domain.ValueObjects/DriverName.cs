using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the driver's name.
/// </summary>
/// <param name="name">The name of the driver.</param>
public class DriverName(string name) : ValueObject<string>(new DriverNameValidator(), name);