using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

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

            return View((FilterProduct(), _context));
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
                    ProductionDate = productInfo.ProductionDate,
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

        private ProductTable[] FilterProduct()
        {
            var results = new List<ProductTable>();
            var query = this.Request.Query;

            if (query.Count > 0)
            {
                foreach (var product in _context.Product)
                {
                    if (query.ContainsKey("ProductName") && product.Name.ToLower().Contains(query["ProductName"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    else if (query.ContainsKey("ProductCategory") && product.Category.ToLower().Contains(query["ProductCategory"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    else if (query.ContainsKey("ProductionDate") && product.ProductionDate.Contains(query["ProductionDate"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    else if (query.ContainsKey("ProductType") && product.ProductType.ToLower().Contains(query["ProductType"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                }
            }
            else
            {
                results.AddRange(_context.Product);
            }

            return results.ToArray();
        }
    }
}
