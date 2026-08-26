using Entities;
using ServiceContracts.DTO;
using System.Security.Claims;

namespace ServiceContracts
{
    public interface IAuthenticationService
    {
        Task<bool> createAccount(AccountRequest request);
        Task<AuthenticationResponse> signIn(LoginRequest request);
        Task signOut();
        Task<string> createRole(string role);
        Task<AuthenticationResponse> validateToken(ClaimsPrincipal principal, string refreshToken);
    }
}
