using System;
using System.Globalization;
using System.Windows;

namespace Wingcode.ValueConverters
{
    /// <summary>
    /// A converted that take a bool and return a s<see cref="Visibility"/>
    /// where false is <see cref="Visibility.Collapsed"/>
    /// </summary>
    public class BooleanInvertToVisibilityGoneConverter : BaseValueConverter<BooleanInvertToVisibilityGoneConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            else
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
