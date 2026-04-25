using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class TariffName(string name) : ValueObject<string>(new TariffNameValidator(), name);
}