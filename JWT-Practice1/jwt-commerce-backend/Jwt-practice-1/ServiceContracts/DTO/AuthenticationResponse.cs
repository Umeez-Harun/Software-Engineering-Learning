using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class AuthenticationResponse
    {
        public string name {  get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public string refreshToken {  get; set; } = string.Empty;
        public DateTime expiresAt { get; set; }
    }
}
