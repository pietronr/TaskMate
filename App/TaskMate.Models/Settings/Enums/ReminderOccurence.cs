using System.ComponentModel;

namespace TaskMate.Models.Settings.Enums
{
    public enum ReminderOccurence
    {
        [Description("minuto(s)")]
        Minutes = 0,
        [Description("hora(s)")]
        Hourly,
    }
}
