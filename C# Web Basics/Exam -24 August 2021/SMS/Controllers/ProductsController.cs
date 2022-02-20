namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.Models.Products;
    using SMS.Services;
    using System.Linq;

    public class ProductsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly IValidator validator;

        public ProductsController(SMSDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }
        [Authorize]
        public HttpResponse Create() => View();


        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateProductFormModel model)
        {
            var modelErrors = this.validator.ValidateProduct(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price
            };

            this.data.Products.Add(product);

            this.data.SaveChanges();

            return Redirect("/Home/IndexLoggedIn");
        }

        [Authorize]
        public HttpResponse Add() => View();

    }
}
