using TaskMate.Models.Tasks.Enums;

namespace TaskMate.Models.Tasks
{
    public class MyTask
    {
        public MyTask()
        {
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }  
        public bool IsCompleted { get; set; }
        public List<Observation> Observations { get; set; } = new List<Observation>();
        public TaskPriority Priority { get; set; }
    }
}
