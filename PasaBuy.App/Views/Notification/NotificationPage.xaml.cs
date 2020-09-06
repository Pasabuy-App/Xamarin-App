using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using PasaBuy.App.DataService;
using System;

namespace PasaBuy.App.Views.Notification
{
    /// <summary>
    /// Page to display Task Notifications list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPage" /> class.
        /// </summary>
        public NotificationPage()
        {
            InitializeComponent();

            this.BindingContext = NotificationDataService.Instance.TaskNotificationViewModel;
        }

        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}