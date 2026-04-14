using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the passenger's name.
/// </summary>
/// <param name="name">The name of the passenger.</param>
public class PassengerName(string name) : ValueObject<string>(new PassengerNameValidator(), name);