using System.Globalization;
using System.Windows.Controls;

namespace Wingcode.Validation.Rules
{
    public class DecimalValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (string.IsNullOrEmpty(value as string))
			{
				return new ValidationResult(isValid: false, "Invalid Value");
			}
			decimal d = decimal.Parse(value as string);
			if (d <= 0m)
			{
				return new ValidationResult(isValid: false, "Invalid Value");
			}
			return ValidationResult.ValidResult;
		}
	}
}
