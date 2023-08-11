namespace TaskMate.Models.Interfaces
{
    public interface ITask
    {
        string Description { get; set; }
        bool IsCompleted { get; set; } 
    }
}
