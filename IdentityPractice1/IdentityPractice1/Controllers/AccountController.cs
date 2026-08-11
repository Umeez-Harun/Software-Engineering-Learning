using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using ServiceContracts.Helper;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Services.Enums;
namespace IdentityPractice1.Controllers
{
    [Route("[controller]/[action]")]
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet("/")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(registerUserRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                throw new Exception("Please ensure all fields are filled correctly");
            }
            ApplicationUser user = new ApplicationUser()
            {
                personName = request.fullName,
                UserName = request.email,
                Email = request.email,
                PhoneNumber = request.phoneNo
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.password);
            if (result.Succeeded)
            {
                if(request.UserType == UserTypeOptions.Admin)
                {
                   ApplicationRole? role = await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString());
                   if(role == null)
                    {
                        IdentityResult result2 =  await _roleManager.CreateAsync(new ApplicationRole { Name = UserTypeOptions.Admin.ToString()});
                        if (result2.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/Admin/Dashboard");
                }
                if(request.UserType == UserTypeOptions.Accountant)
                {
                    ApplicationRole? role =  await _roleManager.FindByNameAsync(UserTypeOptions.Accountant.ToString());
                    if(role == null)
                    {
                        IdentityResult result2 = await _roleManager.CreateAsync(new ApplicationRole { Name = UserTypeOptions.Accountant.ToString() });
                        if (result2.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, UserTypeOptions.Accountant.ToString());
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserTypeOptions.Accountant.ToString());
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/Accounting/Index");
                }
                //sign in the user
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(AdminController.Dashboard), "Admin");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> login(LoginRequest request, string? ReturnUrl)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result =  await _signInManager.PasswordSignInAsync(request.email, request.password, isPersistent: request.rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                //{
                //    return LocalRedirect(ReturnUrl);
                //}
                ApplicationUser? user = await _userManager.FindByEmailAsync(request.email);
                if(user != null && await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                {
                    return RedirectToAction(nameof(AdminController.Dashboard), "Admin");
                }
                if(user != null && await _userManager.IsInRoleAsync(user, UserTypeOptions.Accountant.ToString()))
                {
                    return RedirectToAction(nameof(AccountingController.Index), "Accounting");
                }
                return RedirectToAction(nameof(AdminController.Dashboard), "Admin");
            }
            return View();
        }

        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.login), "Account");
        }
        [AllowAnonymous]
        public async Task<IActionResult> emailAreadyInUse(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
