using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.ViewModels.Helpers;

namespace TaskMate.ViewModels.Tasks
{
    public class MyTaskListViewModel : BindableBase
    {
        public MyTaskListViewModel(bool initialze)
        {
            if (initialze)
            {
                NewTaskCommand = new RelayCommand(NewTask);
            }
        } 

        public MyTaskListViewModel() : this(true)
        {
            CurrentTask = new MyTaskViewModel();
        }

        #region Properties

        private MyTaskViewModel? _currentTask;
        public MyTaskViewModel? CurrentTask
        {
            get => _currentTask;
            set => Set(ref _currentTask, value);
        }

        public ObservableCollection<MyTaskViewModel> MyTasksList { get; set; } = new ObservableCollection<MyTaskViewModel>();

        #endregion

        #region Commands

        public ICommand? NewTaskCommand { get; set; }

        #endregion

        #region Methods

        public void NewTask()
        {
            MyTasksList.Add(CurrentTask!);
            MyTasksList.RefreshCollection();
            CurrentTask = new MyTaskViewModel();
        }

        #endregion
    }
}
