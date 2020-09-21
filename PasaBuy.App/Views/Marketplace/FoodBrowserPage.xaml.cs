using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.DataService;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.StoreDetail;
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
    public partial class FoodBrowserPage : ContentView
    {
        public FoodBrowserPage()
        {
            InitializeComponent();
            this.BindingContext = FoodStoreDataService.Instance.RestaurantViewModel;
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

        private void FoodRestorantTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as FoodStore;

            new Alert("ok", "." + item.Id + ".HAHAHA", "ok");
          
            StoreDetailsViewModel.store_id = item.Id;
            /* StoreDetailsViewModel.loadcategory(item.Id);
             StoreDetailsViewModel.loadstoredetails(item.Id);*/
            StoreDetailsViewModel.loaddata(item.Id);
            StoreDetailsViewModel.loadproduct();
            App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
        }
    }
}