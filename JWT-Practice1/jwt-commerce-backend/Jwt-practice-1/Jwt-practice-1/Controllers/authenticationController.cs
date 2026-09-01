using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Jwt_practice_1.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class authenticationController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthenticationService _authenticationService;

        public authenticationController(IJwtService jwtService, IAuthenticationService authenticationService)
        {
            _jwtService = jwtService;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            AuthenticationResponse response = await _authenticationService.signIn(request);
            return Ok(response);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> signUp(AccountRequest request)
        {
            await _authenticationService.createAccount(request);
            return NoContent();
        }

        [HttpPost("generate-access-token")]
        public async Task<IActionResult> generateAccessToken(TokenRequest request)
        {
            try
            {
                AuthenticationResponse response = await _authenticationService.validateToken(_jwtService.getPrincipal(request.token), request.refreshToken);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return Unauthorized("Please login" + ex.Message);
            }
            
        }
        [HttpGet("logout")]
        public async Task<IActionResult> signOut()
        {
            await _authenticationService.signOut();
            return NoContent();
        }
        [HttpPost("create-role/{roleName}")]
        public async Task<IActionResult> createRole(string roleName)
        {
            string created = await _authenticationService.createRole(roleName);
            return Ok(created);
        }
    }
}
