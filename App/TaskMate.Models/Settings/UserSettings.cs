using TaskMate.Models.Common;
using TaskMate.Models.Settings.Enums;

namespace TaskMate.Models.Settings
{
    public class UserSettings : DbObject
    {
        public bool ReceiveNotifications { get; set; }
        public int DaysBeforeTaskNotification { get; set; }
        public bool RemindMeAtDate { get; set; }
        public int HourOfTheDate { get; set; }  
        public NotificationType NotificationType { get; set; }

        public UserSettings Duplicate()
        {
            UserSettings userSettings = new()
            {
                ReceiveNotifications = ReceiveNotifications,
                DaysBeforeTaskNotification = DaysBeforeTaskNotification,
                RemindMeAtDate = RemindMeAtDate,
                HourOfTheDate = HourOfTheDate,
                NotificationType = NotificationType
            };

            return userSettings;
        }
    }
}
