using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the tariff name (Econom, Comfort, Business, etc.).
/// </summary>
/// <param name="name">The name of the tariff.</param>
public class TariffName(string name) : ValueObject<string>(new TariffNameValidator(), name);