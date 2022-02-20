namespace CarShop.Controllers
{
    using System.Linq;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IValidator validator;

        public CarsController(ApplicationDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        public HttpResponse All()
        {
            var carsQuery = this.data
                .Cars
                .AsQueryable();

            var cars = carsQuery
                .Select(r => new CarListingViewModel
                {

                })
                .ToList();

            return View(cars);
        }
    }
}
