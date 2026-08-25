using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class TokenRequest
    {
        public string token { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
    }
}
