using PasaBuy.App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.Local.Notice
{
    public class LocalNotif
    {
        INotificationManager notificationManager;

        public static LocalNotif instance;
        public static LocalNotif Instance 
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new LocalNotif();
                }
                return instance;
            }
        }

        public LocalNotif()
        {
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
            });
        }


        public void NotifyLocalDevice(string title, string message)
        {
            notificationManager.ScheduleNotification(title, message);
        }
    }
}
