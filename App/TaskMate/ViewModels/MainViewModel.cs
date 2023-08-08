using TaskMate.ViewModels.Tasks;

namespace TaskMate.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            MyTasks = new MyTaskListViewModel();
        }

        public MyTaskListViewModel MyTasks { get; private set; }
    }
}
