using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    public class UserController : Controller
    {
        // Data fields
        private readonly AppDbContext _context;

        /// <summary>
        /// Master constructor
        /// </summary>
        /// <param name="context"></param>
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
                (bool authorize, string userName, string emailAddress, bool isEmployee, int userID) = await LoginUser();

                if(authorize)
                {
                    Global.UserLoggedIn = true;

                    var userInfo = new { UserID = userID, UserName = userName, EmailAddress = emailAddress };
                    Global.UserInfo = userInfo;
                    Global.IsEmployee = isEmployee;

                    this.Response.StatusCode = 1;
                }
                else
                {
                    this.Response.StatusCode = 2;
                }
            }

            return View();
        }

        /// <summary>
        /// Performs the login process.
        /// </summary>
        /// <returns></returns>
        private async Task<(bool, string, string, bool, int)> LoginUser()
        {
            bool authResult = false;

            var reader = new StreamReader(this.Request.Body);
            string content = await reader.ReadToEndAsync();
            reader.Close();

            dynamic? authInfo = JsonConvert.DeserializeObject(content);

            string? userName = authInfo?.UserName;
            string? password = authInfo?.Password;

            string _userName = string.Empty;
            string _email = string.Empty;
            bool isEmployee = false;
            int userID = -1;

            if(userName != null && password != null)
            {
                int userIndex = Utils.FindEmployee(userName, _context.Employee);
                isEmployee = userIndex != -1;

                if (!isEmployee)
                    userIndex = Utils.FindFarmer(userName, _context.Farmer);

                bool userExists = userIndex != -1;

                if (userExists)
                {
                    Employee[] employees = _context.Employee.ToArray();
                    Farmer[] farmers = _context.Farmer.ToArray();

                    userID = isEmployee ? employees[userIndex].EmployeeID : farmers[userIndex].FarmerID;

                    string authPassword = isEmployee ? employees[userIndex].Password : farmers[userIndex].Password;

                    if (password == authPassword)
                    {
                        _userName = isEmployee ? employees[userIndex].UserName : farmers[userIndex].UserName;
                        _email = isEmployee ? employees[userIndex].EmailAddress : farmers[userIndex].EmailAddress;

                        authResult = true;
                    }
                }
            }

            return (authResult, _userName, _email, isEmployee, userID);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            if (this.Request.Method == "POST")
            {
                bool regResult = await RegisterUser();

                if (regResult)
                {
                    this.Response.StatusCode = 1;
                }
                else
                {
                    this.Response.StatusCode = 2;
                }
            }

            return View();
        }

        /// <summary>
        /// Performs the registration process.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Performs a sign-out operation.
        /// </summary>
        /// <returns></returns>
        public IActionResult SignOut()
        {
            Global.UserLoggedIn = false;
            Global.UserInfo = null;
            Global.IsEmployee = false;

            return RedirectToAction("Index", "Home");
        }
    }
}
