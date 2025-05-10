using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    using ProductClass = AgriEnergyConnect.Models.Serializables.Product;
    using ProductTable = AgriEnergyConnect.Models.Tables.Product;

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Product()
        {
            if(this.Request.Method == "POST")
            {
                if (this.Request.Headers["ActionName"] == "AddProduct")
                {
                    await AddProduct();
                    this.Response.StatusCode = 1;
                }

                if (this.Request.Headers["ActionName"] == "RemoveProduct")
                {
                    await RemoveProduct();
                    this.Response.StatusCode = 1;
                }
            }

            return View((_context.Product.ToArray(), _context));
        }

        private async Task AddProduct()
        {
            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            ProductClass? productInfo = JsonConvert.DeserializeObject<ProductClass>(content);

            if(productInfo != null)
            {
                int farmerId = ((dynamic?)Global.UserInfo)?.UserID;

                ProductTable product = new()
                {
                    FarmerID = farmerId,
                    Name = productInfo.Name,
                    Category = productInfo.Category,
                    ProductionDate = DateTime.Parse(productInfo.ProductionDate),
                    ProductType = productInfo.ProductType
                };

                _context.Product.Add(product);
                await _context.SaveChangesAsync();
            }
        }

        private async Task RemoveProduct()
        {
            var reader = new StreamReader(this.Request.Body);
            string productId = await reader.ReadToEndAsync();
            reader.Close();

            int iProductID = Convert.ToInt32(productId);

            ProductTable product = _context.Product.Where(p => p.ProductID == iProductID).ToList()[0];

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
