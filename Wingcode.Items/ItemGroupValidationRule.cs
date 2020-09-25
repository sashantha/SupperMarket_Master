using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wingcode.Data.Rest.Model;

namespace Wingcode.Items
{
    internal class ItemGroupValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is ItemGroup g)
            {
                if (g.id > 0)
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Field is required.");
        }
    }
}
