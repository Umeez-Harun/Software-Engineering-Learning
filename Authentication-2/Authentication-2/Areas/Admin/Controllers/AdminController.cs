using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Authentication_2.Areas.AdminArea.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmployeeService _employeeService;
        public AdminController(IAccountService accountService,IEmployeeService employeeService)
        {
            _accountService = accountService;
            _employeeService = employeeService;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEmployee(Guid? userID)
        {
            return View(new EmployeeRequest() { userID = userID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(EmployeeRequest request)
        {
            EmployeeResponse employee = await _employeeService.AddEmployee(request);
            return RedirectToAction(nameof(AdminController.viewEmployees), "Admin");
        }
        public async Task<IActionResult> viewEmployees()
        {
            List<EmployeeResponse> employees = await _employeeService.getAllEmployees();
            return View(employees);
        }
        [HttpGet]
        [Route("/")]
        public IActionResult createAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createAccount(UserRequest request)
        {
            Guid UserID = await _accountService.createAccount(request);
            return RedirectToAction(nameof(AdminController.AddEmployee), "Admin", new { userID = UserID});
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(Guid employeeID)
        {
            await _employeeService.DeleteEmployee(employeeID);
            return RedirectToAction(nameof(AdminController.viewEmployees), "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> lockUser(Guid userID)
        {
            await _accountService.lockAccount(userID);
            return RedirectToAction(nameof(AdminController.viewEmployees), "Admin");
        }
    }
}
