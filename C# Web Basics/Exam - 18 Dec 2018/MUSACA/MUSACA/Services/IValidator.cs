namespace MUSACA.Services
{
    using System.Collections.Generic;
    using MUSACA.ViewModels.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

    }
}
