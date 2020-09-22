using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.DataService;
using System;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Notification;
using PasaBuy.App.Models.Notification;
using System.Linq;

namespace PasaBuy.App.Views.Notification
{
    /// <summary>
    /// Page to display Task Notifications list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage
    {
        public static int LastIndex = 11;
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPage" /> class.
        /// </summary>
        public NotificationPage()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.Notification.NotificationPageViewModel();
        }

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void TaskNotifications_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as TaskNotification;
            if (NotificationPageViewModel.taskNotificationList.Last() == item && NotificationPageViewModel.taskNotificationList.Count() != 1)
            {
                if (NotificationPageViewModel.taskNotificationList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    NotificationPageViewModel.LoadData(item.ID);
                }
            }
        }
    }
}