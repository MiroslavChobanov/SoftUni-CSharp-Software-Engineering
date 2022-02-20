using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength = 20;

        public const int UsernameMinLength = 5;

        public const string UserEmailRegularExpression = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const int PasswordMinLength = 6;

        public const int SeatsMinValue = 2;
        public const int SeatsMaxValue = 6;

        public const int DescriptionMaxLength = 80;
    }
}
