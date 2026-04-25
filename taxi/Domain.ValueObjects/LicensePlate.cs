using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class LicensePlate(string plate) : ValueObject<string>(new LicensePlateValidator(), plate);
}