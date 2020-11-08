using System;
using System.Globalization;
using System.Windows.Controls;

namespace Wingcode.Validation.Rules
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime time;
            return (!DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time))
                ? new ValidationResult(false, "Date required")
                : ValidationResult.ValidResult;
        }
    }
}
