using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the address.
/// </summary>
/// <param name="address">The address string.</param>
public class Address(string address) : ValueObject<string>(new AddressValidator(), address);