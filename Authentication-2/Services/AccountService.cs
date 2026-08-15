using ServiceContracts;
using ServiceContracts.DTO;
using Microsoft.AspNetCore.Identity;
using Entities;
using Services.Helper;
namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> createAccount(UserRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                throw new ArgumentException("Please fill in All Required fields correctly to create a User Account");
            }
            if (string.IsNullOrEmpty(request.password))
            {
                throw new ArgumentNullException("Password is required", nameof(request.password));
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = request.email,
                Email = request.email,
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Could Not create User Account");
            }
            await assignToRole(user, request.role.ToString());
            return user.Id;
        }

        public async Task<bool> lockAccount(Guid userID)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userID.ToString());
            if(user == null)
            {
                throw new InvalidOperationException("Cannot Find Provided User to Lock Account");
            }
            bool canLock = await _userManager.GetLockoutEnabledAsync(user);
            if (!canLock)
            {
                throw new InvalidOperationException("Cannot lock out user");
            }
            IdentityResult result =  await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return result.Succeeded;
        }

        public async Task<string?> signIn(LoginRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                return null;
            }
            if (string.IsNullOrEmpty(request.password) || string.IsNullOrEmpty(request.email))
            {
                return null;
            }
            
            SignInResult result =  await _signInManager.PasswordSignInAsync(request.email, request.password, isPersistent: false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return null;
            }
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.email);
            if(user == null)
            {
                return null;
            }
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
        public async Task signOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> createRole(string roleName)
        {
            ApplicationRole? role = await _roleManager.FindByNameAsync(roleName);
            if(role != null)
            {
                throw new InvalidOperationException("Role Already exists");
            }
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = roleName });
            return result.Succeeded;
        }
        public async Task<bool> assignToRole(ApplicationUser user, string roleName)
        {
            ApplicationRole? role = await _roleManager.FindByNameAsync(roleName);
            if(role == null || role.Name == null)
            {
                return false;
            }
            ApplicationUser? AppUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if(AppUser == null)
            {
                return false;
            }
            IdentityResult result = await _userManager.AddToRoleAsync(AppUser, role.Name);
            return result.Succeeded;
        }
    }
}
