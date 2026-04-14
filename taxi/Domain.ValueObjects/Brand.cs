using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the car brand.
/// </summary>
/// <param name="brand">The brand of the car.</param>
public class Brand(string brand) : ValueObject<string>(new BrandValidator(), brand); 