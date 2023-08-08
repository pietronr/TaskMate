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

        public ObservableCollection<MyTaskViewModel> TasksList { get; set; } = new ObservableCollection<MyTaskViewModel>();

        #endregion

        #region Commands

        public ICommand? NewTaskCommand { get; set; }   

        #endregion

        #region Methods

        public async Task NewTask()
        {
            await dialogManager!.ShowDialogAsync(new MyTaskViewModel(), new MyTasksDialog(), false, false);
        }

        #endregion
    }
}
