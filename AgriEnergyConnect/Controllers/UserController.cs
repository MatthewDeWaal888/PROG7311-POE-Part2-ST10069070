using AgriEnergyConnect.Models;
using AgriEnergyConnect.Models.Serializables;
using AgriEnergyConnect.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AgriEnergyConnect.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
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

        /// <summary>
        /// Login page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            // Check if the request is 'POST'.
            if (this.Request.Method == "POST")
            {
                // Tuple is used to return the result for 'LoginUser'.
                (bool authorize, string userName, string emailAddress, bool isEmployee, int userID) = await LoginUser();

                // Check if authorization is granted.
                if(authorize)
                {
                    // Perfom the process to sign-in the user.
                    Global.UserLoggedIn = true;

                    var userInfo = new { UserID = userID, UserName = userName, EmailAddress = emailAddress };
                    Global.UserInfo = userInfo;
                    Global.IsEmployee = isEmployee;

                    // The process succeeded.
                    this.Response.StatusCode = 1;
                }
                else
                {
                    // Authorization failed.
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
            // This variable holds the authentication result.
            bool authResult = false;

            // Create a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Read content from the reader.
            string content = await reader.ReadToEndAsync();
            // Close the reader.
            reader.Close();

            // Deserialize the object.
            dynamic? authInfo = JsonConvert.DeserializeObject(content);

            // Obtain the properties from the object.
            string? userName = authInfo?.UserName;
            string? password = authInfo?.Password;

            // Local variables to be used for the Tuple.
            string _userName = string.Empty;
            string _email = string.Empty;
            bool isEmployee = false;
            int userID = -1;

            // Check if the username and password are not null.
            if(userName != null && password != null)
            {
                // Find the employee within the database.
                int userIndex = Utils.FindEmployee(userName, _context.Employee.ToArray());
                isEmployee = userIndex != -1;

                // Check if the user is a farmer.
                if (!isEmployee)
                    userIndex = Utils.FindFarmer(userName, _context.Farmer.ToArray());

                bool userExists = userIndex != -1;

                // Check if the user exists.
                if (userExists)
                {
                    // Declare two arrays that will hold the employees and farmers.
                    Employee[] employees = _context.Employee.ToArray();
                    Farmer[] farmers = _context.Farmer.ToArray();

                    // Get the userID.
                    userID = isEmployee ? employees[userIndex].EmployeeID : farmers[userIndex].FarmerID;

                    // Get the user's password from the database.
                    string authPassword = isEmployee ? employees[userIndex].Password : farmers[userIndex].Password;

                    // Check if the password that the user provided matches the user's password from the database.
                    if (password == authPassword)
                    {
                        _userName = isEmployee ? employees[userIndex].UserName : farmers[userIndex].UserName;
                        _email = isEmployee ? employees[userIndex].EmailAddress : farmers[userIndex].EmailAddress;

                        // The authentication process succeeded.
                        authResult = true;
                    }
                }
            }

            return (authResult, _userName, _email, isEmployee, userID);
        }

        /// <summary>
        /// Register page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Register()
        {
            // Check if the method is 'POST'.
            if (this.Request.Method == "POST")
            {
                // Obtain the registration result.
                bool regResult = await RegisterUser();

                if (regResult)
                {
                    // The registration succeeded.
                    this.Response.StatusCode = 1;
                }
                else
                {
                    // The registration failed.
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
            // This variable will hold the registration status.
            bool regResult = false;

            // Create a StreamReader object.
            var reader = new StreamReader(this.Request.Body);
            // Read the content from the request body (asynchronously).
            string content = await reader.ReadToEndAsync();
            // Close the reader.
            reader.Close();

            // Deserialize a UserRegistration object.
            UserRegistration? regInfo = JsonConvert.DeserializeObject<UserRegistration>(content);

            // Validation
            if (regInfo != null && !Utils.EmployeeExists(regInfo.UserName, _context.Employee.ToArray()))
            {
                // Declare and instantiate an Employee object.
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

                // Add the Employee object to the database.
                _context.Employee.Add(employee);
                // Wait asynchronously.
                await _context.SaveChangesAsync();

                // The registration process completed successfully.
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
            // Perfom the sign-out process.
            Global.UserLoggedIn = false;
            Global.UserInfo = null;
            Global.IsEmployee = false;

            return RedirectToAction("Index", "Home");
        }
    }
}
