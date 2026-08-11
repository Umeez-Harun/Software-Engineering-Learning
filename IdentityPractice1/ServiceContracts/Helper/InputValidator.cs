using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Helper
{
    public static class InputValidator
    {
        public static bool validateInput(object? input)
        {
            if(input == null)
            {
                return false;
            }
            ValidationContext context = new ValidationContext(input);
            List<ValidationResult> result = new List<ValidationResult>();
            bool validation = Validator.TryValidateObject(input, context, result);

            return validation;
        }
    }
}
