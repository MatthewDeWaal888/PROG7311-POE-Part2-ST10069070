using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
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
    }
}
