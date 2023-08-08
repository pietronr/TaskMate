using System.Windows;

namespace TaskMate.Surfaces
{
    public static class DialogExtensions
    {
        #region DialogHostMaxWidth

        public static double GetDialogHostMaxWidth(DependencyObject? obj)
        {
            return (double)obj!.GetValue(DialogHostMaxWidthProperty);
        }
        public static void SetDialogHostMaxWidth(DependencyObject? obj, double value)
        {
            obj!.SetValue(DialogHostMaxWidthProperty, value);
        }

        public static readonly DependencyProperty DialogHostMaxWidthProperty =
            DependencyProperty.RegisterAttached("DialogHostMaxWidth", typeof(double), typeof(DialogExtensions), new FrameworkPropertyMetadata(800d, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region DialogHostMaxHeight

        public static double GetDialogHostMaxHeight(DependencyObject? obj)
        {
            return (double)obj!.GetValue(DialogHostMaxHeightProperty);
        }
        public static void SetDialogHostMaxHeight(DependencyObject? obj, double value)
        {
            obj!.SetValue(DialogHostMaxHeightProperty, value);
        }

        public static readonly DependencyProperty DialogHostMaxHeightProperty =
            DependencyProperty.RegisterAttached("DialogHostMaxHeight", typeof(double), typeof(DialogExtensions), new FrameworkPropertyMetadata(800d, FrameworkPropertyMetadataOptions.Inherits));

        #endregion
    }
}
