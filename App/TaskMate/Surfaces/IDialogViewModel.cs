using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskMate.Surfaces
{
    public interface IDialogViewModel<TResult>
    {
        TaskCompletionSource<MessageDialogResult>? _tcs { get; set; }

        /// <summary>
        /// A task promising the result of the dialog view model. It is completed when <see cref="Close"/> was called.
        /// </summary>
        Task<TResult> DialogTask { get; }
        /// <summary>
        /// Completes the <see cref="DialogTask"/> with the given result and raises the <see cref="Closed"/> event.
        /// </summary>
        void Close(TResult result);
        /// <summary>
        /// This event is raised when the dialog was closed.
        /// </summary>
        event EventHandler? Closed;

        /// <summary>
        /// A command that handles user interaction with the dialog.
        /// </summary>
        ICommand? DialogCommand { get; set; }

        Task OnDialogCommandExecute(MessageDialogResult messageDialogResult);

        bool CanDialogCommandExecute(MessageDialogResult messageDialogResult);

        bool IsDialogCommandExecuting { get; set; }
    }
}
