using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskMate.Models.Tasks;
using TaskMate.Models.Tasks.Enums;
using TaskMate.ViewModels.Helpers;

namespace TaskMate.ViewModels.Tasks
{
    public class MyTaskViewModel : BindableBase
    {
        public MyTaskViewModel(bool initialize)
        {
            if (initialize)
            {
                AddObservationCommand = new RelayCommand(AddObservation);
            }
        }

        public MyTaskViewModel() : this(true)
        {
            Model = new MyTask();
        }

        public MyTaskViewModel(MyTask task) : this(true)
        {
            Model = task;

            Observations = new ObservableCollection<Observation>();

            foreach (Observation observation in task.Observations)
            {
                Observations.Add(observation);
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

        public ObservableCollection<Observation>? Observations { get; private set; }

        #endregion

        #region Commands

        public ICommand? AddObservationCommand { get; set; }

        #endregion

        #region Methods

        public void AddObservation()
        {
            int? count = Observations?.Count;
            Observations!.Add(new Observation($"Item {count + 1}"));
            Observations!.RefreshCollection();
        }

        #endregion
    }
}
