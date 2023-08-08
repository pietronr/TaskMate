using System.ComponentModel;

namespace TaskMate.Models.Tasks.Enums
{
    public enum TaskPriority
    {
        [Description("Prioridade baixa")]
        Low = 0,
        [Description("Prioridade média")]
        Medium,
        [Description("Prioridade alta")]
        High
    }
}
