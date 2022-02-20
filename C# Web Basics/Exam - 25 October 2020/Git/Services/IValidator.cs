namespace Git.Services
{
    using Git.ViewModels.Users;
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
        ICollection<string> ValidateRepository(CreateRepositoryModel model);

    }
}
