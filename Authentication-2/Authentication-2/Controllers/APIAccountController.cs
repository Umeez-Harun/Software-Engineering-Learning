using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace Authentication_2.Controllers
{
    [ApiController]
    [Route("API/[controller]/[action]")]
    public class APIAccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public APIAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> createRole(RoleRequest request)
        {
            if(request.roleName != UserTypeOptions.Admin.ToString() && request.roleName != UserTypeOptions.Employee.ToString())
            {
                return BadRequest("Only Rolenames 'Admin' and 'Employee' are allowed at this time");
            }
            bool isCreated = await _accountService.createRole(request.roleName);
            if(!isCreated)
            {
                return BadRequest("Role could not be created");
            }
            return Ok();
        }
    }
}
