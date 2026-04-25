using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class Model(string model) : ValueObject<string>(new ModelValidator(), model);
}