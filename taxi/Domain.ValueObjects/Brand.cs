using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class Brand(string brand) : ValueObject<string>(new BrandValidator(), brand);
}