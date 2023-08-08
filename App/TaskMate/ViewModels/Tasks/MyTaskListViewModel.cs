using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

        #endregion

        #region Methods

        public async Task NewTask()
        {
            MyTaskViewModel newTask = new()
            {
                DueDate = DateTime.Now,
            };

            await dialogManager!.ShowDialogAsync(newTask, new MyTasksDialog(), false, false);
        }

        #endregion
    }
}
