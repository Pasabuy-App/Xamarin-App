using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Notification
{
    public class TaskNotificationData
    {
        public NotificationData[] data;
        public class NotificationData
        {
            public string id = string.Empty;
            public string wpid = string.Empty;
            public string icon = string.Empty;
            public string activity_title = string.Empty;
            public string activity_info = string.Empty;
            public string open = string.Empty;
            public string date_created = string.Empty;
        }
    }
}
