using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace TaskMate.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public bool ShouldBeEmpty { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!ShouldBeEmpty)
            {
                if (value is int count && count > 0) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
            else
            {
                if (value is int count && count == 0) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
