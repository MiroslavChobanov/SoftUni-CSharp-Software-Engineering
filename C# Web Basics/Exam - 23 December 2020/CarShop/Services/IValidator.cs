namespace CarShop.Services
{
    using CarShop.ViewModels.Users;
    using System.Collections.Generic;
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
    }
}
