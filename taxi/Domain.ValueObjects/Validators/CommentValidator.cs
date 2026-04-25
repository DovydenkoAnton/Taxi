using Domain.ValueObjects.Base;
using Domain.ValueObjects.Exceptions;

namespace Domain.ValueObjects.Validators
{
    public class CommentValidator : IValidator<string>
    {
        public const int MAX_LENGTH = 500;

        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(ExceptionMessages.COMMENT_NOT_NULL_OR_WHITE_SPACE, nameof(value));
            if (value.Length > MAX_LENGTH)
                throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
        }
    }
}