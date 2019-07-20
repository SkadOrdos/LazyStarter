using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LazyStarter
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new ArgumentException("value");

            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
                throw new ArgumentException("value");

            return (Visibility)value == Visibility.Visible;
        }
    }
}
