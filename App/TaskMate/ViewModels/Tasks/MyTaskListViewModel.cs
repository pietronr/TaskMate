using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMate.ViewModels.Helpers;
using TaskMate.Views.Dialogs;

namespace TaskMate.ViewModels.Tasks
{
    public class MyTaskListViewModel : BindableBase
    {
        protected Surfaces.DialogManager? dialogManager;

        public MyTaskListViewModel(bool initialze)
        {
            if (initialze)
            {
                dialogManager = new Surfaces.DialogManager();
                NewTaskCommand = new AsyncRelayCommand(NewTask);
                EditTaskCommand = new AsyncRelayCommand<MyTaskViewModel>(EditTask);
                RemoveTaskCommand = new RelayCommand<MyTaskViewModel>(RemoveTask);
            }
        } 

        public MyTaskListViewModel() : this(true)
        {
        }

        #region Properties

        public ObservableCollection<MyTaskViewModel> TasksList { get; set; } = new ObservableCollection<MyTaskViewModel>();

        #endregion

        #region Commands

        public ICommand? NewTaskCommand { get; set; }

        public ICommand? EditTaskCommand { get; set; }  

        public ICommand? RemoveTaskCommand { get; set; }

        #endregion

        #region Methods

        private async Task NewTask()
        {
            MyTaskViewModel newTask = new()
            {
                DueDate = DateTime.Now,
            };

            await dialogManager!.ShowDialogAsync(newTask, new MyTasksDialog(), false, false);
        }

        private async Task EditTask(MyTaskViewModel task)
        {
            task.IsInEdit = true;   
            await dialogManager!.ShowDialogAsync(task, new MyTasksDialog(), false, false);
        }

        private void RemoveTask(MyTaskViewModel task)
        {
            TasksList.Remove(task);
            TasksList.RefreshCollection();
        }

        #endregion
    }
}
