using System;
using System.Globalization;

namespace Wingcode.Base.ValueConverters
{
    public class InvertBooleanConverter : BaseValueConverter<InvertBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? false : true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
