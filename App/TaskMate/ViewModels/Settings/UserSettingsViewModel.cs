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
            IsNewEntry = true;
        }

        public UserSettingsViewModel(UserSettings model) : this(true)
        {
            Model = model;
            IsNewEntry = false;
        }

        #region Model properties

        private UserSettings? _model;
        public UserSettings? Model
        {
            get => _model;
            set => Set(ref _model, value);
        }

        public bool ReceiveReminders
        {
            get => Model!.ReceiveReminders;
            set
            {
                if (Model!.ReceiveReminders != value)
                {
                    Model.ReceiveReminders = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? DaysBeforeTaskReminder
        {
            get => Model!.DaysBeforeTaskReminder;
            set
            {
                if (Model!.DaysBeforeTaskReminder != value)
                {
                    Model.DaysBeforeTaskReminder = value;
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

        public int? Hour
        {
            get => Model!.Hour;
            set
            {
                if (Model!.Hour != value)
                {
                    if (value < 0 || value > 24)
                    {
                        value = 0;
                    }

                    Model.Hour = value;
                    OnPropertyChanged();
                }
            }
        }

        public int? Minute
        {
            get => Model!.Minute;
            set
            {
                if (Model!.Minute != value)
                {
                    if (value < 0 || value > 60)
                    {
                        value = 0;
                    }

                    Model.Minute = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReminderType? ReminderType
        {
            get => Model!.ReminderType;
            set
            {
                if (Model!.ReminderType != value)
                {
                    Model.ReminderType = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Methods

        public UserSettings GetFullModel()
        {
            return Model!;
        }

        private void ClearValues()
        {
            DaysBeforeTaskReminder = null;
            Hour = null;
            Minute = null;
            ReminderType = null;
            RemindMeAtDate = false;
        }

        public void Close(MessageDialogResult result)
        {
            _tcs!.SetResult(result);
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public async Task<bool> SaveAsync()
        {
            if (!ReceiveReminders)
                ClearValues();

            IsNewEntry = false;

            OnPropertyChanged(null);

            await Task.CompletedTask;

            return true;
        }

        public async Task<bool> RevertChangesAsync()
        {
            UserSettingsViewModel previous = App.MainViewModel.UserSettings.PreviousSettings!;

            if (previous != null)
            {
                ReceiveReminders = previous.ReceiveReminders;
                DaysBeforeTaskReminder = previous.DaysBeforeTaskReminder;
                RemindMeAtDate = previous.RemindMeAtDate;
                Hour = previous.Hour;
                Minute = previous.Minute;
                ReminderType = previous.ReminderType;
                
                IsNewEntry = false;
            }

            OnPropertyChanged(null);

            await Task.CompletedTask;

            return true;
        }

        #endregion
    }
}
