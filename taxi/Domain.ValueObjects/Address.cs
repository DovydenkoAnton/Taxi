using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class Address(string address) : ValueObject<string>(new AddressValidator(), address);
}