namespace CarShop.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UserMinUsername = 4;
        public const int UserMinPassword = 5;
        public const string UserEmailRegularExpression = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        public const string UserTypeClient = "Client";
        public const string UserTypeMechanic = "Mechanic";

        public const int ModelMinLength = 5;

        public const int PlateNumberMaxLength = 8;
        public const string CarPlateNumberRegularExpression = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";

        public const int CarYearMinValue = 1900;
        public const int CarYearMaxValue = 2100;
    }
}
