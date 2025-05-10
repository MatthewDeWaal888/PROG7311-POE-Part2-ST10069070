using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> ManageFarmers()
        {
            if(this.Request.Method == "POST")
            {
                if (this.Request.Headers["ActionName"] == "AddFarmer")
                {
                    bool regResult = await AddFarmer();

                    if(regResult)
                    {
                        this.Response.StatusCode = 1;
                    }
                    else
                    {
                        this.Response.StatusCode = 2;
                    }
                }

                if (this.Request.Headers["ActionName"] == "RemoveFarmer")
                {
                    await RemoveFarmer();
                    this.Response.StatusCode = 1;
                }

                if (this.Request.Headers["ActionName"] == "UpdateFarmer")
                {
                    await UpdateFarmer();
                    this.Response.StatusCode = 1;
                }
            }

            return View(_context.Farmer.ToArray());
        }

        private async Task<bool> AddFarmer()
        {
            bool regResult = false;

            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            UserRegistration? regInfo = JsonConvert.DeserializeObject<UserRegistration>(content);

            if(regInfo != null && !Utils.FarmerExists(regInfo.UserName, _context.Farmer))
            {
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

                _context.Farmer.Add(farmer);
                await _context.SaveChangesAsync();

                regResult = true;
            }

            return regResult;
        }

        private async Task RemoveFarmer()
        {
            var reader = new StreamReader(this.Request.Body);
            string farmerId = await reader.ReadToEndAsync();
            reader.Close();

            int iFarmerID = Convert.ToInt32(farmerId);
            Farmer farmer = _context.Farmer.Where(p => p.FarmerID == iFarmerID).ToList()[0];

            _context.Farmer.Remove(farmer);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateFarmer()
        {
            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            UserProfile? userProfile = JsonConvert.DeserializeObject<UserProfile>(content);

            if(userProfile != null)
            {
                Farmer farmer = _context.Farmer.Where(p => p.FarmerID == userProfile.UserID).ToList()[0];

                // Update the farmer's details.
                farmer.FirstName = userProfile.FirstName;
                farmer.LastName = userProfile.LastName;
                farmer.EmailAddress = userProfile.EmailAddress;
                farmer.CellphoneNumber = userProfile.CellphoneNumber;
                farmer.Gender = userProfile.Gender;
                farmer.UserName = userProfile.UserName;

                _context.Farmer.Update(farmer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
