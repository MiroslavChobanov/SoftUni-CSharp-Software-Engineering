namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Models.Products;
    using SMS.Services;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly SMSDbContext data;

        public HomeController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            SMSDbContext data)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.data = data;
        }
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Home/IndexLoggedIn");
            }

            return View();
        }
        [Authorize]
        public HttpResponse IndexLoggedIn()
        {
            var productsQuery = this.data
                .Products
                .AsQueryable();

            var products = productsQuery
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();


            return View(products);
        }


    }
}