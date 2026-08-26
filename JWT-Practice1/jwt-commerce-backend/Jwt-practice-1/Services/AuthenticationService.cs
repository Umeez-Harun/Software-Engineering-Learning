using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using ServiceContracts.Helper;
using System.Security;
using System.Security.Claims;

namespace Services
{
    public class AuthenticationService :IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _configuration = configuration;
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
            user.UserName = request.email;
            IdentityResult result = await _userManager.CreateAsync(user, request.password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Error! Could Not Create Account, Please try Again Later");
            }
            return await addToRole(user, request.role.ToString());
        }

        public async Task<string> createRole(string role)
        {
            if((role != Role.Buyer.ToString() && role != Role.Seller.ToString()) || string.IsNullOrEmpty(role))
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

        public async Task<AuthenticationResponse> signIn(LoginRequest request)
        {
            bool isValid = InputValidator.validateInput(request);
            if (!isValid)
            {
                throw new ArgumentException("Please fill in required fields correctly");
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(request.email, request.password, isPersistent: false, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                throw new SecurityException("Invalid Credentials, Please try again");
            }
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.email);
            if(user == null)
            {
                throw new InvalidOperationException("An Error occurred, Please try again Later");
            }
            string? role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            if(role == null)
            {
                throw new InvalidOperationException("Error! Could not verify the role of user");
            }
            AuthenticationResponse userResult = _jwtService.generateToken(user, role);
            user.refeshToken = _jwtService.generateRefreshToken();
            user.refreshToken_expirationTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["RefreshToken:EXPIRATION_MINUTES"]!));

            await _userManager.UpdateAsync(user);
            userResult.refreshToken = user.refeshToken;
            return userResult;
        }

        public async Task signOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticationResponse> validateToken(ClaimsPrincipal principal, string refreshToken)
        {
            string? id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            string? role = principal.FindFirstValue(ClaimTypes.Role);
            if (id == null || role == null)
            {
                throw new SecurityException("Error! Invalid Token");
            }
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                throw new InvalidOperationException("Error! Could Not Authenticate user");
            }
            if(refreshToken != user.refeshToken || DateTime.UtcNow > user.refreshToken_expirationTime)
            {
                throw new InvalidOperationException("Error! Refresh Token Has Already Expired! Please Login");
            }
            AuthenticationResponse response = _jwtService.generateToken(user, role);
            response.refreshToken = refreshToken;
            return response;
        }
    }
}
