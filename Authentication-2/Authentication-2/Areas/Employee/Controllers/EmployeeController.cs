using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication_2.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles ="Employee")]
    public class EmployeeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
