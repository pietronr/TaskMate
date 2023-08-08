namespace TaskMate.Models.Tasks
{
    public struct Observation
    {
        public Observation(string? description)
        {
            Description = description;
            IsCompleted = false;
        }

        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
