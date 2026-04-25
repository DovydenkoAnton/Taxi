using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects
{
    public class Comment(string comment) : ValueObject<string>(new CommentValidator(), comment);
}