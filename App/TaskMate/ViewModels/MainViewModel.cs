using TaskMate.ViewModels.Settings;
using TaskMate.ViewModels.Tasks;

namespace TaskMate.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            MyTasks = new MyTaskListViewModel();
            UserSettings = new SettingsShellViewModel();
        }

        #region Main properties

        public MyTaskListViewModel MyTasks { get; private set; }

        public SettingsShellViewModel UserSettings { get; private set; }

        #endregion
    }
}
