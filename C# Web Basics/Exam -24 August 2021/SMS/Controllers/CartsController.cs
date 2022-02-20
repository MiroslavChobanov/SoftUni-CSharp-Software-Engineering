namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.Models.Products;
    using SMS.Services;
    using System.Linq;

    public class CartsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly IValidator validator;

        public CartsController(SMSDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse Details()
        {
            var productsQuery = this.data
                .Products
                .AsQueryable();

            var products = productsQuery
                .Where(p => p.Cart.User.Id == this.User.Id)
                .Select(p => new ProductListingViewModel
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

            return View(products);
        }

        [Authorize]
        public HttpResponse AddProduct(string id, ProductListingViewModel model)
        {
            var productToAdd = this.data
                .Products
                .Where(p => p.Id == model.Id)
                .FirstOrDefault();
            
            var userCart = this.data
                .Carts
                .Where(c => c.User.Id == this.User.Id)
                .FirstOrDefault();

            productToAdd.CartId = userCart.Id;


            this.data.SaveChanges();

            return Redirect("/Carts/Details");
        }

    }
}
