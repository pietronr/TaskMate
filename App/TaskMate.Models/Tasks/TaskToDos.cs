namespace TaskMate.Models.Tasks
{
    public struct TaskToDos
    {
        public TaskToDos(string? description)
        {
            Description = description;
            IsCompleted = false;
        }

        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
