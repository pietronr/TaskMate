using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMate.Models.Tasks;
using TaskMate.Models.Tasks.Enums;
using TaskMate.Surfaces;
using TaskMate.ViewModels.Helpers;

namespace TaskMate.ViewModels.Tasks
{
    public class MyTaskViewModel : BindableBase, IDialogViewModel<MessageDialogResult>
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
                    if (await CreateTask().ConfigureAwait(false))
                    {
                        Close(messageDialogResult);
                    }
                }
                else if (messageDialogResult == MessageDialogResult.Negative)
                {
                    Close(messageDialogResult);
                }
            }
            finally
            {
                IsDialogCommandExecuting = false;
            }

            await Task.CompletedTask;
        }

        public bool CanDialogCommandExecute(MessageDialogResult messageDialogResult) => !IsDialogCommandExecuting;

        #endregion

        public MyTaskViewModel(bool initialize)
        {
            if (initialize)
            {
                _tcs = new TaskCompletionSource<MessageDialogResult>();
                DialogCommand = new AsyncRelayCommand<MessageDialogResult>(OnDialogCommandExecute, CanDialogCommandExecute);
                IsDialogCommandExecuting = false;

                AddObservationCommand = new RelayCommand(AddObservation);
            }
        }

        public MyTaskViewModel() : this(true)
        {
            Model = new MyTask();
        }

        public MyTaskViewModel(MyTask task) : this(true)
        {
            Model = task;

            ToDos = new ObservableCollection<TaskToDos>();

            foreach (TaskToDos observation in task.Observations)
            {
                ToDos.Add(observation);
            }


        }

        #region Model properties

        private MyTask? _model;
        public MyTask? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public string? Title
        {
            get => Model!.Title;
            set
            {
                if (Model!.Title != value)
                {
                    Model!.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? Description
        {
            get => Model!.Description;
            set
            {
                if (Model!.Description != value)
                {
                    Model!.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DueDate
        {
            get => Model!.DueDate;
            set
            {
                if (Model!.DueDate != value)
                {
                    Model!.DueDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get => Model!.IsCompleted;
            set
            {
                if (Model!.IsCompleted != value)
                {
                    Model!.IsCompleted = value;
                    OnPropertyChanged();
                }
            }
        }

        public TaskPriority Priority
        {
            get => Model!.Priority;
            set
            {
                if (Model!.Priority != value)
                {
                    Model!.Priority = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region View properties

        public ObservableCollection<TaskToDos>? ToDos { get; private set; }

        public bool? IsOverdue => DueDate < DateTime.Now;

        #endregion

        #region Commands

        public ICommand? AddObservationCommand { get; set; }

        #endregion

        #region Methods

        public void AddObservation()
        {
            int? count = ToDos?.Count;
            ToDos!.Add(new TaskToDos($"Item {count + 1}"));
            ToDos!.RefreshCollection();
        }

        public void Close(MessageDialogResult result)
        {
            _tcs!.SetResult(result);
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public async Task<bool> CreateTask()
        {
            App.MainViewModel.MyTasks.TasksList?.Add(this);
            App.MainViewModel.MyTasks.TasksList?.RefreshCollection();
            OnPropertyChanged(nameof(IsOverdue));
            await Task.CompletedTask;

            return true;
        }

        #endregion
    }
}
