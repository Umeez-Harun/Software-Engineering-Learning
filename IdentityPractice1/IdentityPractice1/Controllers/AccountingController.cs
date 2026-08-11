using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityPractice1.Controllers
{
    public class AccountingController : Controller
    {
        [Route("[controller]/[action]")]
        [Authorize(Roles="Accountant")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
