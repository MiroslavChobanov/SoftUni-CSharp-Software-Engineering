namespace FootballManager.Controllers
{
    using System.Linq;
    using FootballManager.Data;
    using FootballManager.Data.Models;
    using FootballManager.ViewModels.Players;
    using FootballManager.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class PlayersController : Controller
    {
        private readonly IValidator validator;
        private readonly FootballManagerDbContext data;

        public PlayersController(
            IValidator validator,
            FootballManagerDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            var playersQuery = this.data
                .Players
                .AsQueryable();

            var players = playersQuery
                .Select(p => new PlayerListingViewModel
                {
                    PlayerId = p.Id,
                    FullName = p.FullName,
                    ImageUrl = p.ImageUrl,
                    Position = p.Position,
                    Speed = p.Speed,
                    Endurance = p.Endurance,
                    Description = p.Description
                })
                .ToList();

            return View(players);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddPlayerViewModel model)
        {
            var modelErrors = this.validator.ValidatePlayer(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var player = new Player
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description
            };

            data.Players.Add(player);

            data.SaveChanges();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var userPlayers = this.data
                .UserPlayers
                .Where(up => up.UserId == this.User.Id)
                .Select(up => new PlayerListingViewModel
                {
                    PlayerId = up.PlayerId,
                    FullName = up.Player.FullName,
                    ImageUrl = up.Player.ImageUrl,
                    Position = up.Player.Position,
                    Speed = up.Player.Speed,
                    Endurance = up.Player.Endurance,
                    Description = up.Player.Description
                })
                .ToList();

            return View(userPlayers);
        }

        [Authorize]
        public HttpResponse AddToCollection(int playerId)
        {
            var player = this.data
                .Players
                .FirstOrDefault(p => p.Id == playerId);

            if (data.UserPlayers.Any(up => up.PlayerId == playerId))
            {
                return Error($"You already have {player.FullName} in your collection.");
            }

            var userPlayer = new UserPlayer
            {
                UserId = this.User.Id,
                PlayerId = playerId
            };

            this.data.UserPlayers.Add(userPlayer);

            this.data.SaveChanges();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(int playerId)
        {
            var userPlayer = this.data.UserPlayers.Find(this.User.Id, playerId);

            if (userPlayer == null || userPlayer.UserId != this.User.Id)
            {
                return BadRequest();
            }

            this.data.UserPlayers.Remove(userPlayer);

            this.data.SaveChanges();

            return Redirect("/Players/Collection");
        }
    }
}
