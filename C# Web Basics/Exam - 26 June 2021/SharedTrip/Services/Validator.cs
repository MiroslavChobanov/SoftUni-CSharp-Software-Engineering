namespace SharedTrip.Services
{
    using SharedTrip.Models.Users;
    using SharedTrip.Models.Trips;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using static Data.DataConstants;
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < PasswordMinLength || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.All(x => x == ' '))
            {
                errors.Add($"The provided password cannot be only whitespaces!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidateTrip(CreateTripFormModel model)
        {
            var errors = new List<string>();

            if (model.Seats < SeatsMinValue || model.Seats > SeatsMaxValue)
            {
                errors.Add($"The number of seats is not valid. It must be between {SeatsMinValue} and {SeatsMaxValue} characters long.");
            }

            if (model.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description length should not be over {DescriptionMaxLength}.");
            }

            return errors;
        }
    }
}
