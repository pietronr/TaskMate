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
                    if (await SaveTask().ConfigureAwait(false))
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

                AddToDoCommand = new RelayCommand(AddToDo);
            }
        }

        public MyTaskViewModel() : this(true)
        {
            Model = new MyTask();

            foreach (ToDo todo in Model.ToDos)
            {
                ToDos.Add(new ToDoViewModel(todo));
            }
        }

        public MyTaskViewModel(MyTask task) : this(true)
        {
            Model = task;

            if (task.ToDos.Count > 0)
            {
                foreach (ToDo todo in task.ToDos)
                {
                    ToDos.Add(new ToDoViewModel(todo));
                }
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

        public ObservableCollection<ToDoViewModel> ToDos { get; private set; } =  new ObservableCollection<ToDoViewModel>();

        public bool? IsOverdue => DueDate < DateTime.Now;

        private bool? _isInEdit;
        public bool? IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }

        #endregion

        #region Commands

        public ICommand? AddToDoCommand { get; set; }

        #endregion

        #region Methods

        private void AddToDo()
        {
            int? count = ToDos?.Count;
            ToDos!.Add(new ToDoViewModel(count));
            ToDos!.RefreshCollection();
        }

        public void Close(MessageDialogResult result)
        {
            _tcs!.SetResult(result);
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private async Task<bool> SaveTask()
        {
            if (IsInEdit == false)
            {
                App.MainViewModel.MyTasks.TasksList?.Add(this);
            }

            App.MainViewModel.MyTasks.TasksList?.RefreshCollection();
            OnPropertyChanged(nameof(IsOverdue));

            IsInEdit = false;
            await Task.CompletedTask;

            return true;
        }

        #endregion
    }
}
