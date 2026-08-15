using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace ServiceContracts.DTO
{
    public class EmployeeResponse
    {
        public Guid id { get; set; }

        public string? fullName { get; set; }

        public string? identificationNo { get; set; }

        public string? role { get; set; }

        public Guid? ApplicationUserId { get; set; }

        public bool isDeleted { get; set; }
    }

    public static class EmployeeExtensions
    {
        public static EmployeeResponse convertToEmployeeResponse(this Employee employee)
        {
            return new EmployeeResponse() { id = employee.id, fullName = employee.fullName, identificationNo = employee.identificationNo, ApplicationUserId = employee.ApplicationUserId, isDeleted = employee.isDeleted };
        }
    }
}
