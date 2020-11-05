using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Master
{ 
    internal class BankValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is Bank g)
            {
                if (g.id > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Field is required.");
        }
    }
}
