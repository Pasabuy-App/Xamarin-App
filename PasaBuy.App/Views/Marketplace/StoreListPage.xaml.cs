using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            PageTitle.Text = pageTitle;
            isTapped = false;
        }

        private async void StoreTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Store;
                //App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                //StoreDetailsViewModel.loadcategory(item.Id);
                //StoreDetailsViewModel.loadstoredetails(item.Id);

                StoreDetailsViewModel.store_id = item.Id;
                StoreDetailsViewModel.Loadcategory(item.Id);
                StoreDetailsViewModel.Loadstoredetails(item.Id);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                isTapped = false;
            }
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

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void StoreList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Store;
            if (StoreBrowserViewModel.storeList.Last() == item && StoreBrowserViewModel.storeList.Count() != 1)
            {
                if (StoreBrowserViewModel.storeList.IndexOf(item) >= LastIndex)
                {
                    LastIndex += 6;
                    StoreBrowserViewModel.LoadStore(catid, item.Id);
                }
            }
        }
    }
}