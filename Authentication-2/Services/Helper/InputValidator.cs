using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class InputValidator
    {
        public static bool validateInput(Object? obj)
        {
            if(obj == null)
            {
                return false;
            }
            ValidationContext context = new ValidationContext(obj);
            List<ValidationResult> results = new List<ValidationResult>();
            bool result = Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

            return result;
        }
    }
}
