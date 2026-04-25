using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class DriverName(string name) : ValueObject<string>(new DriverNameValidator(), name);
}