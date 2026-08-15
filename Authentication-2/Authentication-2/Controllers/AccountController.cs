using Authentication_2.Areas.AdminArea.Controllers;
using Authentication_2.Areas.Employee.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Authentication_2.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [AllowAnonymous]
        [Route("/")]
        [HttpGet]
        public IActionResult login()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginRequest request)
        {
            ViewBag.Errors = new List<string>();
            string? role = await _accountService.signIn(request);
            if(role == null)
            {
                ViewBag.Errors.Add("Error! Could not figure out the User Role");
            }
            if(role == UserTypeOptions.Admin.ToString())
            {
                return RedirectToAction(nameof(AdminController.Dashboard), "Admin", new { area = "Admin"});
            }
            else if(role == UserTypeOptions.Employee.ToString())
            {
                return RedirectToAction(nameof(EmployeeController.Dashboard),"Employee", new { area = "Employee"});
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await _accountService.signOut();
            return LocalRedirect("/");
        }
    }
}
