namespace MUSACA.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;

        public const string RoleAdminType = "Admin";
        public const string RoleUserType = "User";

        public const string UserEmailRegularExpression = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        
        public const int BarcodeLength = 12;

        public const string StatusActiveType = "Active";
        public const string StatusCompletedType = "Completed";
    }
}
