using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IAuthenticationService
    {
        Task<bool> createAccount(AccountRequest request);
        Task<bool> signIn(LoginRequest request);
        Task signOut();
        Task<string> createRole(string role);

    }
}
