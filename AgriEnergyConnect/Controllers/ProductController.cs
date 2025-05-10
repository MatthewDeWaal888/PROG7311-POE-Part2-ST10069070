using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnect.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Product()
        {
            if(this.Request.Method == "POST")
            {

            }

            return View((_context.Product.ToArray(), _context));
        }

        private async Task AddProduct()
        {

        }
    }
}
