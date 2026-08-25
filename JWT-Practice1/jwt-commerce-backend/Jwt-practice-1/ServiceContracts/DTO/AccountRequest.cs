using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class AccountRequest
    {
        public string name {  get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public Role role { get; set; } = Role.Buyer;

        public ApplicationUser convertToApplicationUser()
        {
            return new ApplicationUser() { name = name, UserName = email, Email = email };
        }
    }
}
