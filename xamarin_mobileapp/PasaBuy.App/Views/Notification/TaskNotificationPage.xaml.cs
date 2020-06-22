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
    public partial class TaskNotificationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotificationPage" /> class.
        /// </summary>
        public TaskNotificationPage()
        {
            InitializeComponent();

            this.BindingContext = TaskNotificationDataService.Instance.TaskNotificationViewModel;
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