namespace Domain.ValueObjects.Exceptions
{
    internal static class ExceptionMessages
    {
        public const string PASSENGER_NAME_NOT_NULL_OR_WHITE_SPACE = "The Passenger name mustn't be null, empty or consists only of white-space characters";
        public const string DRIVER_NAME_NOT_NULL_OR_WHITE_SPACE = "The Driver name mustn't be null, empty or consists only of white-space characters";
        public const string BRAND_NOT_NULL_OR_WHITE_SPACE = "The Brand mustn't be null, empty or consists only of white-space characters";
        public const string MODEL_NOT_NULL_OR_WHITE_SPACE = "The Model mustn't be null, empty or consists only of white-space characters";
        public const string LICENSE_PLATE_NOT_NULL_OR_WHITE_SPACE = "The License plate mustn't be null, empty or consists only of white-space characters";
        public const string ADDRESS_NOT_NULL_OR_WHITE_SPACE = "The Address mustn't be null, empty or consists only of white-space characters";
        public const string TARIFF_NAME_NOT_NULL_OR_WHITE_SPACE = "The Tariff name mustn't be null, empty or consists only of white-space characters";
        public const string COMMENT_NOT_NULL_OR_WHITE_SPACE = "The Comment mustn't be null, empty or consists only of white-space characters";
        public const string VALIDATOR_MUST_BE_SPECIFIED = "Validator must be specified for type";
    }
}