using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is a required field")]
        [EmailAddress(ErrorMessage = "Email is a required field")]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is a required field")]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;
        public bool rememberMe { get; set; }
    }
}
