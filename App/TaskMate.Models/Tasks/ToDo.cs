using TaskMate.Models.Common;

namespace TaskMate.Models.Tasks
{
    public class ToDo : DbObject
    {
        public ToDo(string? description)
        {
            Description = description;
        }

        public MyTask? MyTask { get; set; }  
        public int MyTaskId { get; set; } 
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }   
    }
}
