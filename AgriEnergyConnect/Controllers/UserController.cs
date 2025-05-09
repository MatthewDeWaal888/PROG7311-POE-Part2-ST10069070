using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            if (this.Request.Method == "POST")
            {
                bool authorize = await LoginUser();

                if(authorize)
                {

                }
            }

            return View();
        }

        private async Task<bool> LoginUser()
        {
            bool authResult = false;

            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            dynamic? authInfo = JsonConvert.DeserializeObject(content);

            string? userName = authInfo?.UserName;
            string? password = authInfo?.Password;

            if(userName != null && password != null)
            {
                int userIndex = Utils.FindEmployee(userName, _context.Employee);
                bool isEmployee = userIndex != -1;

                if (!isEmployee)
                    userIndex = Utils.FindFarmer(userName, _context.Farmer);

                bool userExists = userIndex != -1;

                if (userExists)
                {
                    string authPassword = isEmployee ? _context.Employee.ToArray<Employee>()[userIndex].Password : _context.Farmer.ToArray<Farmer>()[userIndex].Password;

                    if(password == authPassword)
                        authResult = true;
                }
            }

            return authResult;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            if (this.Request.Method == "POST")
            {
                bool regResult = await RegisterUser();

                if (regResult)
                    return RedirectToAction("Login", "User");
            }

            return View();
        }

        private async Task<bool> RegisterUser()
        {
            bool regResult = false;

            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            UserRegistration? regInfo = JsonConvert.DeserializeObject<UserRegistration>(content);

            if (regInfo != null && !Utils.EmployeeExists(regInfo.UserName, _context.Employee))
            {
                Employee employee = new()
                {
                    FirstName = regInfo.FirstName,
                    LastName = regInfo.LastName,
                    EmailAddress = regInfo.EmailAddress,
                    CellphoneNumber = regInfo.CellphoneNumber,
                    Gender = regInfo.Gender.ToString(),
                    UserName = regInfo.UserName,
                    Password = regInfo.Password
                };

                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                regResult = true;
            }

            return regResult;
        }
    }
}
