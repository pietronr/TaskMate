using TaskMate.Models.Tasks.Enums;

namespace TaskMate.Models.Tasks
{
    public class Task
    {
        public Task()
        {
        }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public List<Observation> Observations { get; set; } = new List<Observation>();
        public TaskPriority Priority { get; set; }
    }
}
