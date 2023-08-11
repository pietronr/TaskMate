using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMate.ViewModels.Helpers;
using TaskMate.Views.Dialogs;
using TaskMate.Models.Tasks.Enums;
using TaskMate.Models.Tasks;
using System.Linq;

namespace TaskMate.ViewModels.Tasks
{
    public class MyTaskListViewModel : BindableBase
    {
        protected Surfaces.DialogManager? dialogManager;

        public MyTaskListViewModel(bool initialize)
        {
            if (initialize)
            {
                dialogManager = new Surfaces.DialogManager();
                NewTaskCommand = new AsyncRelayCommand(NewTask);
                EditTaskCommand = new AsyncRelayCommand<MyTaskViewModel>(EditTask);
                RemoveTaskCommand = new RelayCommand<MyTaskViewModel>(RemoveTask);
                CompleteTaskCommand = new RelayCommand<MyTaskViewModel>(CompleteTask);
                ShowTaskToDosCommand = new RelayCommand<MyTaskViewModel>(ShowTaskToDos);
            }
        } 

        public MyTaskListViewModel() : this(true)
        {
            ObservableCollection<ToDoViewModel> todos = new()
            {
                new ToDoViewModel("Finalizar projeto"),
                new ToDoViewModel("Preparar apresentação")
            };

            MyTaskViewModel firstTask = new()
            {
                Title = "Tarefa UFG",
                Description = "Projeto de Arquitetura de Software",
                DueDate = new(2023, 9, 10, 10, 10, 10, DateTimeKind.Utc),
                Priority = TaskPriority.High,
                ToDos = todos,
            };

            TasksList.Add(firstTask);
        }

        #region Properties

        public ObservableCollection<MyTaskViewModel> TasksList { get; set; } = new ObservableCollection<MyTaskViewModel>();

        private MyTaskViewModel? _currentTask;
        public MyTaskViewModel? CurrentTask
        {
            get => _currentTask;
            set => Set(ref _currentTask, value);
        }

        #endregion

        #region Commands

        public ICommand? NewTaskCommand { get; set; }

        public ICommand? EditTaskCommand { get; set; }  

        public ICommand? RemoveTaskCommand { get; set; }

        public ICommand? CompleteTaskCommand { get;set; }   

        public ICommand? ShowTaskToDosCommand { get; set; }

        #endregion

        #region Methods

        private async Task NewTask()
        {
            MyTaskViewModel newTask = new()
            {
                DueDate = DateTime.Now,
            };

            CurrentTask = newTask;

            await dialogManager!.ShowDialogAsync(newTask, new MyTasksDialog(), false, false);
        }

        private async Task EditTask(MyTaskViewModel task)
        {
            task.IsInEdit = true;

            MyTask duplicate = task.GetFullModel().Duplicate(task.ToDos.Select(x => x.Model).ToList()!);

            CurrentTask = new MyTaskViewModel(duplicate);

            await dialogManager!.ShowDialogAsync(task, new MyTasksDialog(), false, false);
        }

        private void RemoveTask(MyTaskViewModel task)
        {
            TasksList.Remove(task);
            TasksList.RefreshCollection();
        }

        private void CompleteTask(MyTaskViewModel task)
        {
            task.IsCompleted = true;
        }

        private void ShowTaskToDos(MyTaskViewModel task)
        {
            if (task.ToDos.Count > 0)
            {
                task.ShowToDos = !task.ShowToDos;
            }
        }

        #endregion
    }
}
