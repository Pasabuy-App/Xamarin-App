
using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodBrowserPage : ContentView
    {
        public bool isTapped = false;
        public static int LastIndex = 12;
        public FoodBrowserPage()
        {
            InitializeComponent();
            this.BindingContext = new FoodBrowserViewModel();
            SearchEntry.Completed += (sender, args) => SearchStore(sender, args);
            //FoodBrowserViewModel.foodstorelist.CollectionChanged += CollectionChanges;

            StoreListStack.Scrolled += OnCollectionViewScrolled;
        }
        async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex >= LastIndex)
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    FoodBrowserViewModel.SearchStore("", LastIndex.ToString());
                    LastIndex += 7;
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
            }
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
                    FoodBrowserViewModel.foodstorelist.Clear();
                    FoodBrowserViewModel.SearchStore(SearchEntry.Text, "");
                    ClearSearch();
                    await Task.Delay(500);
                    IsRunning.IsRunning = false;
                }
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

    }
}