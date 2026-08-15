using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IAccountService
    {
        public Task<Guid> createAccount(UserRequest request);
        public Task<string?> signIn(LoginRequest request);
        public Task signOut();
        public Task<bool> lockAccount(Guid userID);
        public Task<bool> createRole(string roleName);

        public Task<bool> assignToRole(ApplicationUser user, string roleName);
    }
}
