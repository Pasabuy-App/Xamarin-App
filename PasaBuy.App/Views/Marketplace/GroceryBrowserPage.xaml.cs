using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroceryBrowserPage : ContentView
    {
        public static bool isTapped = false;
        public static int LastIndex = 11;
        public GroceryBrowserPage()
        {
            InitializeComponent();
            isTapped = false;
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
            //this.BindingContext = StoreDataService.Instance.RestaurantViewModel;


        }
        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            LastIndex = 11;
            GroceryBrowserViewModel.grocerystorelist.Clear();
            GroceryBrowserViewModel.LoadGrocery("");
            pullToRefresh.IsRefreshing = false;
        }
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


        private async void GroceriesTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Groceries;
                StoreDetailsViewModel.store_id = item.Id;
                StoreDetailsViewModel.Loadcategory(item.Id);
                StoreDetailsViewModel.Loadstoredetails(item.Id);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await System.Threading.Tasks.Task.Delay(200);
                isTapped = false;
            }
        }

        private void RestaurantList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Groceries;
            if (GroceryBrowserViewModel.grocerystorelist.Last() == item && GroceryBrowserViewModel.grocerystorelist.Count() != 1)
            {
                if (GroceryBrowserViewModel.grocerystorelist.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    GroceryBrowserViewModel.LoadGrocery(item.Id);
                }
            }
        }

    }
}