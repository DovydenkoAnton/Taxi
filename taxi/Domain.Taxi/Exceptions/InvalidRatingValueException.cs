namespace Domain.Taxi.Exceptions;

public class InvalidRatingValueException : ArgumentException
{
    public decimal Rating { get; }

    public InvalidRatingValueException(decimal rating)
        : base($"Rating value {rating} is invalid. Rating must be between 1 and 5.")
    {
        Rating = rating;
    }
}