namespace FootballManager.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using FootballManager.ViewModels.Players;
    using FootballManager.ViewModels.Users;

    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < DefaultMinLength || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {DefaultMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Email.Length < EmailMinLength || model.Email.Length > EmailMaxLength)
            {
                errors.Add($"Email '{model.Email}' is not valid. It must be between {EmailMinLength} and {EmailMaxLength} characters long.");
            }


            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < DefaultMinLength || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {DefaultMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }

        public ICollection<string> ValidatePlayer(AddPlayerViewModel model)
        {
            var errors = new List<string>();

            if (model.FullName.Length < DefaultMinLength || model.FullName.Length > FullNameMaxLength)
            {
                errors.Add($"Full name '{model.FullName}' is not valid. It must be between {DefaultMinLength} and {FullNameMaxLength} characters long.");
            }

            if (model.Position.Length < DefaultMinLength || model.Position.Length > DefaultMaxLength)
            {
                errors.Add($"Position '{model.Position}' is not valid. It must be between {DefaultMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Speed < 0 || model.Speed > MaxSpeed)
            {
                errors.Add($"Speed '{model.Speed}' is not valid. It must be between 0 and {MaxSpeed}.");
            }

            if (model.Endurance < 0 || model.Endurance > MaxEndurance)
            {
                errors.Add($"Endurance '{model.Endurance}' is not valid. It must be between 0 and {MaxEndurance}.");
            }

            if (model.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description should be less than {DescriptionMaxLength} characters long.");
            }
            return errors;
        }

    }
}
