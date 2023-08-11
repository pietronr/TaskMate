using TaskMate.Models.Common;
using TaskMate.Models.Settings.Enums;

namespace TaskMate.Models.Settings
{
    public class Settings : DbObject
    {
        public bool ReceiveNotifications { get; set; }
        public int DaysBeforeTaskNotification { get; set; }
        public bool RemindMeAtDate { get; set; }
        public int HourOfTheDate { get; set; }  
        public NotificationType Type { get; set; }
    }
}
