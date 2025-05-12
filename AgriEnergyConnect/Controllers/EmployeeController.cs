using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    /// <summary>
    /// Employee Controller
    /// </summary>
    public class EmployeeController : Controller
    {
        // Data fields.
        private readonly AppDbContext _context;

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="context"></param>
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ManageFarmers page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> ManageFarmers()
        {
            // Check if the request is a POST method.
            if(this.Request.Method == "POST")
            {
                // Check if the action is 'AddFarmer'.
                if (this.Request.Headers["ActionName"] == "AddFarmer")
                {
                    // Get the registration result.
                    bool regResult = await AddFarmer();

                    if(regResult)
                    {
                        // The registration process is a success.
                        this.Response.StatusCode = 1;
                    }
                    else
                    {
                        // The registration process failed.
                        this.Response.StatusCode = 2;
                    }
                }

                // Check if the action is 'RemoveFarmer'.
                if (this.Request.Headers["ActionName"] == "RemoveFarmer")
                {
                    // Wait for the asynchronous operation to complete.
                    await RemoveFarmer();
                    // The process succeeded.
                    this.Response.StatusCode = 1;
                }

                // Check if the action is 'UpdateFarmer'.
                if (this.Request.Headers["ActionName"] == "UpdateFarmer")
                {
                    // Wait for the asynchronous operation to complete.
                    await UpdateFarmer();
                    // The process failed.
                    this.Response.StatusCode = 1;
                }
            }

            return View(_context.Farmer.ToArray());
        }

        /// <summary>
        /// This method handles the operation to add a farmer.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> AddFarmer()
        {
            // Declare a variable that will hold the registration status.
            bool regResult = false;

            // Create a StreamReader object to read data from the request body.
            var reader = new StreamReader(this.Request.Body);
            // Read the content from the request body (asynchronously).
            string content = await reader.ReadToEndAsync();
            // Close the StreamReader object.
            reader.Close();

            // Deserialize the UserRegistration.
            UserRegistration? regInfo = JsonConvert.DeserializeObject<UserRegistration>(content);

            // Validation: Check if the regInfo is not null and if the farmer does not exist.
            if(regInfo != null && !Utils.FarmerExists(regInfo.UserName, _context.Farmer.ToArray()))
            {
                // Create a Farmer object.
                Farmer farmer = new()
                {
                    FirstName = regInfo.FirstName,
                    LastName = regInfo.LastName,
                    EmailAddress = regInfo.EmailAddress,
                    CellphoneNumber = regInfo.CellphoneNumber,
                    Gender = regInfo.Gender.ToString(),
                    UserName = regInfo.UserName,
                    Password = regInfo.Password
                };

                // Save the Farmer object to the database.
                _context.Farmer.Add(farmer);
                // Wait asynchronously.
                await _context.SaveChangesAsync();

                // Set the registration result to true.
                regResult = true;
            }

            return regResult;
        }
        
        /// <summary>
        /// This method handles the operation to remove a farmer.
        /// </summary>
        /// <returns></returns>
        private async Task RemoveFarmer()
        {
            // Create a StreamReader object to read the content of the request body.
            var reader = new StreamReader(this.Request.Body);
            // Obtain the farmer id.
            string farmerId = await reader.ReadToEndAsync();
            // Close the StreamReader object.
            reader.Close();

            // Convert a string into an integer.
            int iFarmerID = Convert.ToInt32(farmerId);
            // Obtain the farmer from the filter operation.
            Farmer farmer = _context.Farmer.Where(p => p.FarmerID == iFarmerID).ToList()[0];

            _context.Product.RemoveRange(_context.Product.Where(p => p.FarmerID == farmer.FarmerID));
            _context.Farmer.Remove(farmer);

            // Wait asynchronously.
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method handles the operation to update a farmer.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateFarmer()
        {
            // Create a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Read the content from the request body.
            string content = await reader.ReadToEndAsync();
            // Close the StreamReader object.
            reader.Close();

            // Deserialize the UserProfile object.
            UserProfile? userProfile = JsonConvert.DeserializeObject<UserProfile>(content);

            // Check if the userProfile object is not null.
            if(userProfile != null)
            {
                // Create a Farmer object.
                Farmer farmer = _context.Farmer.Where(p => p.FarmerID == userProfile.UserID).ToList()[0];

                // Update the farmer's details.
                farmer.FirstName = userProfile.FirstName;
                farmer.LastName = userProfile.LastName;
                farmer.EmailAddress = userProfile.EmailAddress;
                farmer.CellphoneNumber = userProfile.CellphoneNumber;
                farmer.Gender = userProfile.Gender;
                farmer.UserName = userProfile.UserName;

                _context.Farmer.Update(farmer);

                // Wait asynchronously.
                await _context.SaveChangesAsync();
            }
        }
    }
}
