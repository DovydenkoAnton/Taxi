using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the car model.
/// </summary>
/// <param name="model">The model of the car.</param>
public class Model(string model) : ValueObject<string>(new ModelValidator(), model);