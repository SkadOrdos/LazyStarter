using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LazyStarter
{
    [ValueConversion(typeof(FrameworkElement), typeof(FrameworkElement))]
    class ResizeElementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sizeString = parameter.ToString().Split(';');

            var element = (FrameworkElement)value;
            element.Width = Int32.Parse(sizeString[0]);
            element.Height = Int32.Parse(sizeString[1]);
            return element;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
