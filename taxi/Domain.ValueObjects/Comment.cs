using Domain.ValueObjects.Base;
using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects;

/// <summary>
/// Represents type of the feedback comment.
/// </summary>
/// <param name="comment">The comment text.</param>
public class Comment(string comment) : ValueObject<string>(new CommentValidator(), comment);