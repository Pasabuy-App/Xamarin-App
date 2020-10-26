using Plugin.LocalNotification;
using System;

namespace PasaBuy.App.Local.Notice
{
    public class LocalNotif
    {
        public static LocalNotif instance;
        public static LocalNotif Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalNotif();
                }
                return instance;
            }
        }

        public LocalNotif()
        {
            // Local Notification received event listener
            NotificationCenter.Current.NotificationReceived += OnLocalNotificationReceived;

            // Local Notification tap event listener
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        public void Show(string title, string description, string data)
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = data,
                Description = description,
                ReturningData = data
            };
            NotificationCenter.Current.Show(notification);
        }

        public void Show(string title, string description, string data, DateTime date)
        {
            TimeSpan tspan = date.Subtract(DateTime.Now);
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = data,
                Description = description,
                ReturningData = data,
                NotifyTime = DateTime.Now.Add(tspan)
            };
            NotificationCenter.Current.Show(notification);
        }

        private void OnLocalNotificationReceived(NotificationReceivedEventArgs e)
        {
            Console.WriteLine("Demoguy! Received Notification => " + e.Title + " Description: " + e.Description);
        }

        private void OnLocalNotificationTapped(NotificationTappedEventArgs e)
        {
            Console.WriteLine("Demoguy! Tapped Notification => " + e.Data);
        }
    }
}
