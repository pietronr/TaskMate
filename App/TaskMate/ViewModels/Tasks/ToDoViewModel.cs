using TaskMate.Models.Tasks;

namespace TaskMate.ViewModels.Tasks
{
    public class ToDoViewModel : BindableBase
    {
        public ToDoViewModel(int? count)
        {
            Model = new ToDo($"Item {count}");
        }

        public ToDoViewModel(ToDo model)
        {
            Model = model;
            MyTask = model.MyTask;
        }

        #region Model properties

        private ToDo? _model;
        public ToDo? Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private MyTask? _myTask;
        public MyTask? MyTask
        {
            get => _myTask;
            set => Set(ref _myTask, value);
        }

        public string? Description
        {
            get => Model!.Description;
            set
            {
                if (Model!.Description != value)
                {
                    Model.Description = value;
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
                    Model.IsCompleted = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}
