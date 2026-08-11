using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Enums;


namespace ServiceContracts.DTO
{
    public class registerUserRequest
    {
        [Required(ErrorMessage = "Name can't be blank")]
        public string? fullName {  get; set; }

        [Required(ErrorMessage = "email can't be blank")]
        [EmailAddress(ErrorMessage = "Please enter correct Email Format")]
        [Remote(action: "emailAreadyInUse", controller: "Account", ErrorMessage = "Email Already in Use")]
        public string? email {  get; set; }

        [Required(ErrorMessage = "phone Number can't be blank")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Numbers should contain numericals only")]
        public string? phoneNo { get; set; }

        [Required(ErrorMessage = "password can't be blank")]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password and confirm password do not match")]
        public string confirmPassword { get; set; } = string.Empty;
        public UserTypeOptions UserType { get; set; } = UserTypeOptions.Admin;
    }
}
