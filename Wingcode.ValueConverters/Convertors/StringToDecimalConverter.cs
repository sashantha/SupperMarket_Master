using System;
using System.Globalization;

namespace Wingcode.ValueConverters
{
    public class StringToDecimalConverter : BaseValueConverter<StringToDecimalConverter>
	{
		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "0.00";
			}
			if (value is decimal)
			{
				return ((decimal)value).ToString("#,#0.00#;(#,#0.00#)");
			}
			return "0.00";
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return 0.00m;
			}
            decimal.TryParse((string)value, out decimal result);
            return result;
		}
	}
}
