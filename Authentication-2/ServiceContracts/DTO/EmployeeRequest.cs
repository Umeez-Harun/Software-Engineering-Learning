using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    public class EmployeeRequest
    {
        [Required(ErrorMessage ="Full Name is a required field")]
        public string? fullName { get; set; }

        [Required(ErrorMessage = "Identification NO is a required field")]
        public string? identificatonNo { get; set; }
        public Guid? userID { get; set; }
        public Employee convertToEmployee()
        {
            return new Employee() {  fullName = fullName, identificationNo = identificatonNo};
        }
    }

    
}
