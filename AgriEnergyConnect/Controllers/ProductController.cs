using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace AgriEnergyConnect.Controllers
{
    // Aliases given to these classes 'Serializables.Product' and 'Tables.Product'.
    using ProductClass = AgriEnergyConnect.Models.Serializables.Product;
    using ProductTable = AgriEnergyConnect.Models.Tables.Product;

    /// <summary>
    /// Product Controller
    /// </summary>
    public class ProductController : Controller
    {
        // Data fields
        private readonly AppDbContext _context;

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Product page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Product()
        {
            // Check if the request method is 'POST'.
            if(this.Request.Method == "POST")
            {
                // Check if the action is 'AddProduct'.
                if (this.Request.Headers["ActionName"] == "AddProduct")
                {
                    // Wait asynchronously.
                    await AddProduct();
                    // Return an OK status.
                    this.Response.StatusCode = 1;
                }

                if (this.Request.Headers["ActionName"] == "RemoveProduct")
                {
                    // Wait asynchronously.
                    await RemoveProduct();
                    // Return an OK status.
                    this.Response.StatusCode = 1;
                }
            }

            return View((FilterProduct(), _context));
        }

        /// <summary>
        /// Handles the operation to add a product to the database.
        /// </summary>
        /// <returns></returns>
        private async Task AddProduct()
        {
            // Create a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Read the content from the reader, asynchronously.
            string content = await reader.ReadToEndAsync();
            // Close the reader.
            reader.Close();

            // Create a Product object
            ProductClass? productInfo = JsonConvert.DeserializeObject<ProductClass>(content);

            // Check if the object is not null.
            if(productInfo != null)
            {
                // Get the farmer id.
                int farmerId = ((dynamic?)Global.UserInfo)?.UserID;

                // Create a ProductTable object.
                ProductTable product = new()
                {
                    FarmerID = farmerId,
                    Name = productInfo.Name,
                    Category = productInfo.Category,
                    ProductionDate = productInfo.ProductionDate,
                    ProductType = productInfo.ProductType
                };

                _context.Product.Add(product);

                // Wait asynchronously for the operation to complete.
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Handles the process to remove a product from the database.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveProduct()
        {
            // Declare and instantiate a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Obtain the product id.
            string productId = await reader.ReadToEndAsync();
            // Close the reader.
            reader.Close();

            // Convert a string into an integer.
            int iProductID = Convert.ToInt32(productId);

            // Get the product from the database.
            ProductTable product = _context.Product.Where(p => p.ProductID == iProductID).ToList()[0];

            // Remove the product from the DbSet collection.
            _context.Product.Remove(product);
            // Wait asynchronously.
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Handles the process to filter products from the database.
        /// </summary>
        /// <returns></returns>
        private ProductTable[] FilterProduct()
        {
            // Declare a new List.
            var results = new List<ProductTable>();
            // Alias given to 'this.Request.Query'.
            var query = this.Request.Query;

            // Check if the URL has parameters.
            if (query.Count > 0)
            {
                // Iterate through the Product collection.
                foreach (var product in _context.Product)
                {
                    // Check if the URL contains the 'ProductName' parameter.
                    if (query.ContainsKey("ProductName") && product.Name.ToLower().Contains(query["ProductName"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    // Check if the URL contains the 'ProductCategory' parameter.
                    else if (query.ContainsKey("ProductCategory") && product.Category.ToLower().Contains(query["ProductCategory"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    // Check if the URL contains the 'ProductionDate' parameter.
                    else if (query.ContainsKey("ProductionDate") && product.ProductionDate.Contains(query["ProductionDate"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                    // Check if the URL contains the 'ProductType' parameter.
                    else if (query.ContainsKey("ProductType") && product.ProductType.ToLower().Contains(query["ProductType"].ToString().ToLower()))
                    {
                        results.Add(product);
                    }
                }
            }
            else
            {
                // Add all the items to the result list, if the URL contains no parameters.
                results.AddRange(_context.Product);
            }

            return results.ToArray();
        }
    }
}
