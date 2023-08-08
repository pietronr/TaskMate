using System.Globalization;
using System.Windows;
using System;
using System.Windows.Data;

namespace TaskMate.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool ShowIfFalse { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!ShowIfFalse)
            {
                if ((bool)value) return Visibility.Visible;
                else return Visibility.Collapsed;
            }
            else
            {
                if ((bool)value) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
