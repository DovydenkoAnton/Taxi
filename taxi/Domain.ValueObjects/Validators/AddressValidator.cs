using Domain.ValueObjects.Base;
using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects.Validators
{
    public class AddressValidator : IValidator<string>
    {
        public const int MAX_LENGTH = 200;
        public const int MIN_LENGTH = 3;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.ADDRESS_NOT_NULL_OR_WHITE_SPACE, nameof(value));
            if (value.Length > MAX_LENGTH)
                throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
            if (value.Length < MIN_LENGTH)
                throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
        }
    }
}