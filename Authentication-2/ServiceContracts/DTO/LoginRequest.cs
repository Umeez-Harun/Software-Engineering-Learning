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
        [EmailAddress(ErrorMessage = "Please enter the correct email format")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}
