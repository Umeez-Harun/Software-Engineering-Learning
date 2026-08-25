using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string name {  get; set; } = string.Empty;
        public string refeshToken { get; set; } = string.Empty;
        public DateTime refreshToken_expirationTime { get; set; }
    }
}
