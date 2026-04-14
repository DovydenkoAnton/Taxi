using Domain.Taxi.Base;
using Domain.Taxi.Exceptions;
using Domain.ValueObjects;

namespace Domain.Taxi.Entities;

/// <summary>
/// Represents a tariff type (Econom, Comfort, Business, etc.).
/// </summary>
public class Tariff : Entity<Guid>
{
    public TariffName Name { get; private set; }
    public string? Description { get; private set; }

    protected Tariff() { }

    public Tariff(Guid id, TariffName name, string? description = null) : base(id)
    {
        Name = name ?? throw new ArgumentNullValueException(nameof(name));
        Description = description;
    }

    public Tariff(TariffName name, string? description = null)
        : this(Guid.NewGuid(), name, description) { }
}