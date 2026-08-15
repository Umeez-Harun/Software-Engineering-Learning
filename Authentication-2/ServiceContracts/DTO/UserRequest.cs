using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Email is a required field")]
        [EmailAddress(ErrorMessage = "Please enter correct Email Format")]
        public string? email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string? password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is a required field")]
        [DataType(DataType.Password, ErrorMessage = "Please enter correct password format")]
        [Compare("password", ErrorMessage = "Password and Confirm Password must match")]
        public string? confirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is a required field")]
        public UserTypeOptions role { get; set; }
    }
}
