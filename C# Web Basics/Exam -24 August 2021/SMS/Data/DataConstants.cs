namespace SMS.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const int UsernameMinLength = 5;

        public const string UserEmailRegularExpression = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        
        public const int PasswordMinLength = 6;

        public const int ProductNameMinLength = 4;

        public const int DefaultMaxLength = 20;

        public const decimal MinPrice = 0.05M;
        public const decimal MaxPrice = 1000M;

    }
}
