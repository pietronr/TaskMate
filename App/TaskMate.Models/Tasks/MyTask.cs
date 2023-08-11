using TaskMate.Models.Common;
using TaskMate.Models.Interfaces;
using TaskMate.Models.Tasks.Enums;

namespace TaskMate.Models.Tasks
{
    public class MyTask : DbObject, ITask
    {
        public MyTask()
        {
            ToDos.Add(new ToDo("Item 1"));
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }  
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public List<ToDo> ToDos { get; set; } = new List<ToDo>();

        public MyTask Duplicate(List<ToDo> todos)
        {
            MyTask myTask = new()
            {
                Title = Title,
                Description = Description,
                IsCompleted = IsCompleted,
                DueDate = DueDate,
                Priority = Priority,
                ToDos = todos,
            };

            return myTask;
        }
    }
}
