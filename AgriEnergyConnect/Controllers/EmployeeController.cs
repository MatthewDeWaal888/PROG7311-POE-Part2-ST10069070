using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnect.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult ManageFarmers()
        {
            return View();
        }
    }
}
