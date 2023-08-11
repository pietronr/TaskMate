using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMate.Models.Settings;
using TaskMate.Models.Settings.Enums;
using TaskMate.Surfaces;
using TaskMate.ViewModels.Helpers;

namespace TaskMate.ViewModels.Settings
{
    public class UserSettingsViewModel : BindableBase, IDialogViewModel<MessageDialogResult>, IDatabasePersist
    {
        #region IDialogViewModel members

        public ICommand? DialogCommand { get; set; }
        public TaskCompletionSource<MessageDialogResult>? _tcs { get; set; }
        public Task<MessageDialogResult> DialogTask => _tcs!.Task;
        public event EventHandler? Closed;
        public bool IsDialogCommandExecuting { get; set; }

        public async Task OnDialogCommandExecute(MessageDialogResult messageDialogResult)
        {
            try
            {
                IsDialogCommandExecuting = true;

                if (messageDialogResult == MessageDialogResult.Affirmative)
                {
                    if (await SaveAsync().ConfigureAwait(false))
                    {
                        Close(messageDialogResult);
                    }
                }
                else if (messageDialogResult == MessageDialogResult.Negative && await RevertChangesAsync().ConfigureAwait(false))
                {
                    Close(messageDialogResult);
                }
            }
            finally
            {
                IsDialogCommandExecuting = false;
            }
        }

        public bool CanDialogCommandExecute(MessageDialogResult messageDialogResult) => !IsDialogCommandExecuting;

        #endregion

        public UserSettingsViewModel(bool initialze)
        {
            if (initialze)
            {
                _tcs = new TaskCompletionSource<MessageDialogResult>();
                DialogCommand = new AsyncRelayCommand<MessageDialogResult>(OnDialogCommandExecute, CanDialogCommandExecute);
                IsDialogCommandExecuting = false;
            }
        }

        public UserSettingsViewModel() : this(true)
        {
            Model = new UserSettings();
        }

        public UserSettingsViewModel(UserSettings model) : this(true)
        {
            Model = model;
        }

        #region Model properties

        private UserSettings? _model;
        public UserSettings? Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        public bool ReceiveNotifications
        {
            get => Model!.ReceiveNotifications;
            set
            {
                if (Model!.ReceiveNotifications != value)
                {
                    Model.ReceiveNotifications = value;
                    OnPropertyChanged();
                }
            }
        }

        public int DaysBeforeTaskNotification
        {
            get => Model!.DaysBeforeTaskNotification;
            set
            {
                if (Model!.DaysBeforeTaskNotification != value)
                {
                    Model.DaysBeforeTaskNotification = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool RemindMeAtDate
        {
            get => Model!.RemindMeAtDate;
            set
            {
                if (Model!.RemindMeAtDate != value)
                {
                    Model.RemindMeAtDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int HourOfTheDate
        {
            get => Model!.HourOfTheDate;
            set
            {
                if (Model!.HourOfTheDate != value)
                {
                    Model.HourOfTheDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public NotificationType NotificationType
        {
            get => Model!.NotificationType;
            set
            {
                if (Model!.NotificationType != value)
                {
                    Model.NotificationType = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region View properties

        #endregion

        #region Methods

        public void Close(MessageDialogResult result)
        {
            _tcs!.SetResult(result);
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevertChangesAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
