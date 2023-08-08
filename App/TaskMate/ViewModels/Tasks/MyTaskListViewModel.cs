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
                AddTaskCommand = new RelayCommand(AddTask);
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

        private bool _isCreating;
        public bool IsCreating
        {
            get => _isCreating;
            set => Set(ref _isCreating, value);
        }

        public ObservableCollection<MyTaskViewModel> MyTasksList { get; set; } = new ObservableCollection<MyTaskViewModel>();

        #endregion

        #region Commands

        public ICommand? NewTaskCommand { get; set; }   

        public ICommand? AddTaskCommand { get; set; }

        #endregion

        #region Methods

        public void NewTask()
        {
            IsCreating = true;
        }

        public void AddTask()
        {
            MyTasksList.Add(CurrentTask!);
            MyTasksList.RefreshCollection();
            CurrentTask = new MyTaskViewModel();
            IsCreating = false;
        }

        #endregion
    }
}
