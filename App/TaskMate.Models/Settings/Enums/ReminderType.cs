using System.ComponentModel;

namespace TaskMate.Models.Settings.Enums
{
    public enum ReminderType
    {
        [Description("Simples")]
        Simple = 0,
        [Description("Moderada")]
        Moderate,
        [Description("Complexa")]
        Complex
    }
}
