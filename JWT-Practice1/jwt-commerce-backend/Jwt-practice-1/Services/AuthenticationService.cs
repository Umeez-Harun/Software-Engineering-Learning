using Entities;
using Microsoft.AspNetCore.Identity;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using ServiceContracts.Helper;

namespace Services
{
    public class AuthenticationService :IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> addToRole(ApplicationUser user, string role)
        {
            if (user == null || string.IsNullOrEmpty(role))
            {
                throw new InvalidOperationException("User and role can't be null!");
            }
            ApplicationRole? role_res = await _roleManager.FindByNameAsync(role);
            ApplicationUser? User = await _userManager.FindByIdAsync(user.Id.ToString());
            if(User == null || role_res == null)
            {
                throw new InvalidOperationException("User and role don't exist");
            }
            IdentityResult result = await _userManager.AddToRoleAsync(User, role);
            return result.Succeeded;
        }

        public async Task<bool> createAccount(AccountRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                throw new ArgumentException("Please fill in required fields correctly");
            }
            ApplicationUser user = request.convertToApplicationUser();
            IdentityResult result = await _userManager.CreateAsync(user, request.password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error! Could Not Create Account, Please try Again Later");
            }
            await addToRole(user, request.role.ToString());
            return true;
        }

        public async Task<string> createRole(string role)
        {
            if(role != Role.Buyer.ToString() || role != Role.Seller.ToString() || string.IsNullOrEmpty(role))
            {
                throw new ArgumentException("Please select a valid role {Buyer or Seller} only");
            }
            ApplicationRole? rol = await _roleManager.FindByNameAsync(role);
            if(rol != null)
            {
                throw new InvalidOperationException("Role already exists");
            }
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole { Name = role });
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error! Could Not Create Role, Please try Again Later");
            }
            return role;
        }

        public async Task<bool> signIn(LoginRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                throw new ArgumentException("Please fill in required fields correctly");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(request.email, request.password, isPersistent: false, lockoutOnFailure: true);
            //generate jwt token after jwt service implementation
            return result.Succeeded;
        }

        public async Task signOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
