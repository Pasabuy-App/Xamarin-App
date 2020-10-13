using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.ViewModels.Chat;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Chat
{
    /// <summary>
    /// Page to show recent chat list.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        public static bool isFirstID = false;
        public static int ids = 0;
        public static int LastIndex = 11;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePage" /> class.
        /// </summary>
        public MessagePage()
        {
            InitializeComponent();
            LastIndex = 11;
            isFirstID = false;
            ids = 0;

            //pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        /*private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            LastIndex = 11;
            RecentChatViewModel.chatItems.Clear();
            RecentChatViewModel.LoadMesssage("");
            pullToRefresh.IsRefreshing = false;
        }*/

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (SearchBox.IsVisible)
                {
                    SearchBox.WidthRequest = width;
                }
            }
        }

        /// <summary>
        /// Invoked when back button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            if (this.TitleBar != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    SearchBox.WidthRequest = property;
                    opacity = property / TitleBar.Width;
                    SearchBox.Opacity = opacity;
                },
                TitleBar.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(SearchBox, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;
        }

        /// <summary>
        /// Invokes when search box Animation completed.
        /// </summary>
        private void SearchBoxAnimationCompleted()
        {
            this.SearchBox.IsVisible = false;
            this.ProfileView.IsVisible = true;
        }

        /// <summary>
        /// Invoked when search button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {  
            this.ProfileView.IsVisible = false;

            if (this.TitleBar != null)
            {
                double opacity;

                // Animating Width of the search box, from 0 to full width when it added to the view.
                var expandAnimation = new Animation(
                    property =>
                    {
                        SearchBox.WidthRequest = property;
                        opacity = property / TitleBar.Width;
                        SearchBox.Opacity = opacity;
                    }, 0, TitleBar.Width, Easing.Linear);
                expandAnimation.Commit(SearchBox, "Expand", 16, 250, Easing.Linear, (p, q) => this.SearchExpandAnimationCompleted());
            }
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        private void SearchExpandAnimationCompleted()
        {
            this.SearchEntry.Focus();
        }

        private void ListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            try
            {
                var item = e.ItemData as ChatDetail;
                if (RecentChatViewModel.chatItems.Last() == item && RecentChatViewModel.chatItems.Count() != 1)
                {
                    if (RecentChatViewModel.chatItems.IndexOf(item) >= LastIndex)
                    {
                        if (isFirstID)
                        {
                            ids += 7;
                        }
                        else
                        {
                            isFirstID = true;
                        }
                        LastIndex += 6;
                        RecentChatViewModel.LoadMesssage(ids.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}