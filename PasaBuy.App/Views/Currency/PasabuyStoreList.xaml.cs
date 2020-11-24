using PasaBuy.App.ViewModels.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasabuyStoreList : ContentPage
    {
        public static int LastIndex = 12;
        public PasabuyStoreList()
        {
            InitializeComponent();
            this.BindingContext = new PasabuyStoreListViewModel();
            SearchEntry.Completed += (sender, args) => SearchStore(sender, args);
            StoreList.Scrolled += OnCollectionViewScrolled;
        }

        public void ClearSearch()
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

        public async void SearchStore(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SearchEntry.Text))
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    PasabuyStoreListViewModel._storeList.Clear();
                    PasabuyStoreListViewModel.LoadMore(SearchEntry.Text, "");
                    ClearSearch();
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
            }
        }
        async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex >= LastIndex)
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    PasabuyStoreListViewModel.LoadMore("", LastIndex.ToString());
                    LastIndex += 7;
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
            }
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
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
    }
}