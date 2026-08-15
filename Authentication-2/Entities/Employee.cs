using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee
    {
        public Guid id {  get; set; }


        [Required]
        [StringLength(30)]
        public string? fullName { get; set; }

        [Required]
        [StringLength(30)]
        public string? identificationNo { get; set; } 

        public Guid? ApplicationUserId { get; set; }

        public bool isDeleted { get; set; } = false;
     }
}
