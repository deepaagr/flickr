using System;
using System.Windows;
using System.Windows.Data;

namespace ImageSearchWPF.Converters
{
    /// <summary>
    /// Converter to set the visibility property of textblock based on the passed value
    /// </summary>
    public class TextBlockVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isVisible = (bool)value;
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
