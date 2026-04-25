using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class PassengerName(string name) : ValueObject<string>(new PassengerNameValidator(), name);
}