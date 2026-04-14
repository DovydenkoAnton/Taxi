namespace Domain.Taxi.Exceptions;

public class ArgumentNullValueException : ArgumentNullException
{
    public ArgumentNullValueException(string paramName)
        : base(paramName, $"Argument \"{paramName}\" value is null")
    {
    }
}