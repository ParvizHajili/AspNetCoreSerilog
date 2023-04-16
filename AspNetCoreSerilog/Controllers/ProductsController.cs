using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AspNetCoreSerilog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult TestError()
        {
            throw new Exception("This is test exception.");
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            products.Add(product);
            Log.Information("New Product added!!!");

            return Ok(products);
        }
    }

    public class Product
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
    }
}
