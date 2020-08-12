﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Wingcode.Base.ValueConverters
{
    /// <summary>
    /// Converter that takes in an rgb string and converts it to RGB brush
    /// </summary>
    public class StringRGBToBrushConverter : BaseValueConverter<StringRGBToBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom($"#{value}"));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
