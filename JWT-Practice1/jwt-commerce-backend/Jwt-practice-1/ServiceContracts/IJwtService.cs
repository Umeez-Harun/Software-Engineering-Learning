using Entities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse generateToken(ApplicationUser user, string role);
        ClaimsPrincipal getPrincipal(string token);
        string generateRefreshToken();
    }
}
