using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
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
                    ProductionDate = DateTime.TryParse(productInfo.ProductionDate, out DateTime d) ? d : DateTime.Now,
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
            // Checks if dt1 contains dt2.
            bool containsDateTime(DateTime? dt1, DateTime? dt2, bool dateIncluded, bool timeIncluded)
            {
                // The full date-time of dt1.
                string? dt1Str = dt1?.ToString();

                // The date part of dt2.
                string? dt2DateStr = dt2?.ToString("yyyy/MM/dd");
                // The time part of dt2.
                string? dt2TimeStr = dt2?.ToString("HH:mm:ss");

                bool result = false;

                // Checks if the user provided date and time.
                if(dateIncluded && timeIncluded)
                {
                    result = dt1Str.Contains(dt2DateStr) && dt1Str.Contains(dt2TimeStr);
                }
                // Checks if the user provided the date only.
                else if(dateIncluded && !timeIncluded)
                {
                    result = dt1Str.Contains(dt2DateStr);
                }
                // Checks if the user provided the time only.
                else if(!dateIncluded && timeIncluded)
                {
                    result = dt1Str.Contains(dt2TimeStr);
                }

                return result;
            }

            // Declare a new List.
            var results = new List<ProductTable>();
            // Alias given to 'this.Request.Query'.
            var query = this.Request.Query;

            // Check if the URL has parameters.
            if (query.Count > 0)
            {
                // Declare a ProductTable object.
                ProductTable[]? products = null;

                // Check if the query contains the parameter 'FarmerID'.
                if (query.ContainsKey("FarmerID"))
                {
                    // Filter all products for a specific farmer.
                    int farmerId = Convert.ToInt32(query["FarmerID"]);
                    products = _context.Product.Where(p => p.FarmerID == farmerId).ToArray();
                }
                else
                {
                    // Filter all products for all farmers.
                    products = _context.Product.ToArray();
                }

                // Check if the query only contains the 'FarmerID' parameter.
                if (query.ContainsKey("FarmerID") && query.Count == 1)
                {
                    results.AddRange(products);
                }
                else
                {
                    // Iterate through the Product collection.
                    foreach (var product in products)
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
                        else if (query.ContainsKey("ProductionDate"))
                        {
                            // Check if the ProductionDate filter contains a date range.
                            if (query["ProductionDate"].ToString().Contains(";"))
                            {
                                // Get the parts of the date range.
                                string[] parts = query["ProductionDate"].ToString().Split(';');

                                // Check if the array contains two parts and if the two parts can be parsed to a DateTime object.
                                if (parts.Count() == 2 && DateTime.TryParse(parts[0], out DateTime part1) && DateTime.TryParse(parts[1], out DateTime part2))
                                {
                                    // Check if the ProductionDate of the product variable is within the date range.
                                    if (product.ProductionDate >= part1 && product.ProductionDate <= part2)
                                    {
                                        results.Add(product);
                                    }
                                }
                            }
                            // The ProductionDate filter does not contain a date range.
                            else if (DateTime.TryParse(query["ProductionDate"].ToString(), out DateTime d) && containsDateTime(product.ProductionDate, d, query["ProductionDate"].ToString().Contains("/"), query["ProductionDate"].ToString().Contains(":")))
                            {
                                results.Add(product);
                            }
                        }
                        // Check if the URL contains the 'ProductType' parameter.
                        else if (query.ContainsKey("ProductType") && product.ProductType.ToLower().Contains(query["ProductType"].ToString().ToLower()))
                        {
                            results.Add(product);
                        }
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
