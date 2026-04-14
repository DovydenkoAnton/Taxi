namespace Domain.Taxi.Exceptions;

public class InvalidRatingValueException(decimal rating)
    : ArgumentException($"Rating value {rating} is invalid. Rating must be between 1 and 5.")
{
    public decimal Rating => rating;
}