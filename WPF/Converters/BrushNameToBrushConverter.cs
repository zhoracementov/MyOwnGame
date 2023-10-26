using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF.Converters
{
    public class BrushNameToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)typeof(Brushes).GetProperty((string)value).GetValue(null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
