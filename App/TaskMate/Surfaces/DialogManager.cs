using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace TaskMate.Surfaces
{
    public class DialogManager
    {
        public async Task<TResult> ShowDialogAsync<TResult>(IDialogViewModel<TResult> viewModel, object view, bool strechHorizontally = true, bool stretchVertically = false)
        {
            if (view is not UserControl dialog)
            {
                throw new InvalidOperationException($"The view {view.GetType()} belonging to view model {viewModel.GetType()} does not inherit from {typeof(UserControl)}");
            }

            dialog.DataContext = viewModel;

            CustomDialog customDialog = new()
            {
                Content = dialog,
                Style = (Style)Application.Current.Resources["CustomDialogStyle"],
                HorizontalContentAlignment = strechHorizontally ? HorizontalAlignment.Stretch : HorizontalAlignment.Center,
                VerticalContentAlignment = stretchVertically ? VerticalAlignment.Stretch : VerticalAlignment.Center
            };

            DialogExtensions.SetDialogHostMaxWidth(customDialog, DialogExtensions.GetDialogHostMaxWidth(view as UserControl));
            DialogExtensions.SetDialogHostMaxHeight(customDialog, DialogExtensions.GetDialogHostMaxHeight(view as UserControl));
            var firstMetroWindow = Application.Current.Windows.OfType<MetroWindow>().First();
            await firstMetroWindow.ShowMetroDialogAsync(customDialog, null);
            await Task.Delay(200);
            customDialog.Focus();
            TResult result;

            try
            {
                result = await viewModel.DialogTask;
            }
            finally
            {
                await firstMetroWindow.HideMetroDialogAsync(customDialog, null);
            }

            viewModel._tcs = new TaskCompletionSource<MessageDialogResult>();

            return result;
        }
    }
}
