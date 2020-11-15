using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wingcode.ValueConverters
{
    public class DateTimeToStringConverter : BaseValueConverter<DateTimeToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToShortDateString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            DateTime result = DateTime.Now;
            if (s.Contains("/"))
                DateTime.TryParse(s, out result);
            return result;
        }
    }
}
