namespace SharedTrip.Controllers
{
    using System.Linq;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System;
    using SharedTrip.Models.UserTrips;

    public class TripsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public TripsController(ApplicationDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var tripsQuery = this.data
                .Trips
                .AsQueryable();

            var trips = tripsQuery
                .Select(t => new TripListingViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    Details = t.Description
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();


        [HttpPost]
        [Authorize]
        public HttpResponse Add(CreateTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.Parse(model.DepartureTime),
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description
            };

            this.data.Trips.Add(trip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = this.data
                .Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsFormModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("s"),
                    ImagePath = t.ImagePath,
                    Seats = t.Seats,
                    Description = t.Description
                })
                .FirstOrDefault();

            if (trip == null)
            {
                return BadRequest();
            }

            return View(trip);

        }

        [Authorize]
        public HttpResponse AddUserToTrip(string id, UserTripFormModel model)
        {
            var userTrip = new UserTrip
            {
                UserId = User.Id,
                TripId = model.TripId
            };

            var trip = data
                .Trips
                .FirstOrDefault(t => t.Id == model.TripId);

            trip.Seats -= 1;

            if (trip.Seats <= 2)
            {
                return Error("No more space my friend.");
            }

            this.data.UserTrips.Add(userTrip);

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
