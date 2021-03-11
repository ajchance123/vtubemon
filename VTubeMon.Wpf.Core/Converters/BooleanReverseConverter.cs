using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VTubeMon.Wpf.Core.Converters
{
    public class BooleanReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue) return value;

            bool b = (bool)value;
            return !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
