namespace SMS.Services
{
    using SMS.Models.Products;
    using SMS.Models.Users;
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

        public ICollection<string> ValidateProduct(CreateProductFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < ProductNameMinLength || model.Name.Length > DefaultMaxLength)
            {
                errors.Add($"Product name '{model.Name}' is not valid. It must be between {ProductNameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (model.Price < MinPrice || model.Price > MaxPrice)
            {
                errors.Add($"Price '{model.Price}' is not valid. It must be between {MinPrice} and {MaxPrice}.");
            }

            return errors;
        }
    }
}
