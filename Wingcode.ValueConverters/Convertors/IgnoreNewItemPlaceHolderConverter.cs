using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wingcode.ValueConverters
{
	public class IgnoreNewItemPlaceHolderConverter : BaseValueConverter<IgnoreNewItemPlaceHolderConverter>
	{
		private const string NewItemPlaceholderName = "{NewItemPlaceholder}";

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && value.ToString() == NewItemPlaceholderName)
			{
				return DependencyProperty.UnsetValue;
			}
			return value;
		}
	}
}
