namespace Domain.ValueObjects.Exceptions
{
    public class ArgumentNullOrWhiteSpaceException(string message, string paramName)
        : ArgumentNullException(paramName, message)
    {
    }
}