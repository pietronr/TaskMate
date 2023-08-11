using TaskMate.Models.Common;
using TaskMate.Models.Settings.Enums;

namespace TaskMate.Models.Settings
{
    public class UserSettings : DbObject
    {
        public bool ReceiveReminders { get; set; }
        public int? DaysBeforeTaskReminder { get; set; }
        public bool RemindMeAtDate { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public ReminderType? ReminderType { get; set; }

        public UserSettings Duplicate()
        {
            UserSettings userSettings = new()
            {
                ReceiveReminders = ReceiveReminders,
                DaysBeforeTaskReminder = DaysBeforeTaskReminder,
                RemindMeAtDate = RemindMeAtDate,
                Hour = Hour,
                Minute = Minute,
                ReminderType = ReminderType
            };

            return userSettings;
        }
    }
}
