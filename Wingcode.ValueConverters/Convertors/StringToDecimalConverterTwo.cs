using System;
using System.Globalization;

namespace Wingcode.ValueConverters
{
    public class StringToDecimalConverterTwo : BaseValueConverter<StringToDecimalConverterTwo>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "0.000";
			}
			if (value is decimal)
			{
				return ((decimal)value).ToString("#,#0.000#;(#,#0.000#)");
			}
			return "0.000";
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return 0.000m;
			}
			decimal result = 0.000m;
			decimal.TryParse((string)value, out result);
			return result;
		}
	}
}
