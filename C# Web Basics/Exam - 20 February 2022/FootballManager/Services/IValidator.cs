namespace FootballManager.Services
{
    using System.Collections.Generic;
    using FootballManager.ViewModels.Players;
    using FootballManager.ViewModels.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
        ICollection<string> ValidatePlayer(AddPlayerViewModel model);

    }
}
