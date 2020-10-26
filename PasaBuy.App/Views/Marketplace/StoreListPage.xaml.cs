using PasaBuy.App.ViewModels.Marketplace;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreListPage : ContentPage
    {
        public static string pageTitle;
        public static int LastIndex = 11;
        public static string catid = string.Empty;
        public bool isTapped;
        public StoreListPage()
        {
            InitializeComponent();
            this.BindingContext = new StoreListViewModel();
            PageTitle.Text = pageTitle;
            isTapped = false;
            SearchEntry.Completed += (sender, args) => SearchStore(sender, args);
            StoreListViewModel.storeList.CollectionChanged += CollectionChanges;
        }
        private void CollectionChanges(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    Search.WidthRequest = property;
                    opacity = property / TitleView.Width;
                    Search.Opacity = opacity;
                },
                TitleView.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;
        }
        public void SearchStore(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SearchEntry.Text))
                {
                    StoreListViewModel.SearchStore(SearchEntry.Text);
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }

        /*private async void StoreTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Store;
                //App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                //StoreDetailsViewModel.loadcategory(item.Id);
                //StoreDetailsViewModel.loadstoredetails(item.Id);

                StoreDetailsViewModel.store_id = item.Id;
                //StoreDetailsViewModel.Loadcategory(item.Id);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                isTapped = false;
            }
        }*/

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Search.IsVisible)
                {
                    Search.WidthRequest = width;
                }
            }
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            this.Search.IsVisible = true;
            this.Title.IsVisible = false;
            this.SearchButton.IsVisible = false;

            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from 0 to full width when it added to the view.
                var expandAnimation = new Animation(
                    property =>
                    {
                        Search.WidthRequest = property;
                        opacity = property / TitleView.Width;
                        Search.Opacity = opacity;
                    }, 0, TitleView.Width, Easing.Linear);
                expandAnimation.Commit(Search, "Expand", 16, 250, Easing.Linear, (p, q) => this.SearchExpandAnimationCompleted());
            }
        }

        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    Search.WidthRequest = property;
                    opacity = property / TitleView.Width;
                    Search.Opacity = opacity;
                },
                TitleView.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;
        }

        private void SearchBoxAnimationCompleted()
        {
            this.Search.IsVisible = false;
            this.Title.IsVisible = true;
        }

        private void SearchExpandAnimationCompleted()
        {
            this.SearchEntry.Focus();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void StoreList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            /*var item = e.ItemData as Store;
            if (StoreListViewModel.storeList.Last() == item && StoreListViewModel.storeList.Count() != 1)
            {
                if (StoreListViewModel.storeList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    StoreListViewModel.LoadMore(catid, item.Id);
                }
            }*/
        }
    }
}