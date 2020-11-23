using PasaBuy.App.ViewModels.Marketplace;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerBrowserPage : ContentView
    {
        public static bool isTapped = false;
        public static int LastIndex = 12;
        public PartnerBrowserPage()
        {
            InitializeComponent();
            this.BindingContext = new PartnerBrowserViewModel();
            isTapped = false;

            SearchEntry.Completed += (sender, args) => SearchStore(sender, args);
            //PartnerBrowserViewModel.storeList.CollectionChanged += CollectionChanges;
            StoreList.Scrolled += OnCollectionViewScrolled;
        }
        async void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.LastVisibleItemIndex >= LastIndex)
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    PartnerBrowserViewModel.SearchStore(SearchEntry.Text, LastIndex.ToString());
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
                    PartnerBrowserViewModel.storeList.Clear();
                    PartnerBrowserViewModel.SearchStore(SearchEntry.Text, "");
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

        /*private async void Partner_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
                *//*var item = e.ItemData as Categories;
                PartnerListViewModel.LoadStore(item.Id, "");
                PartnerListPage.catid = item.Id;
                PartnerListPage.pageTitle = item.Title;
                await App.Current.MainPage.Navigation.PushModalAsync(new PartnerListPage());*//*
           if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as Store;
                StoreDetailsViewModel.store_id = item.Id;
                //StoreDetailsViewModel.Loadcategory(item.Id);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await System.Threading.Tasks.Task.Delay(200);
                isTapped = false;
            }

        }*/
    }
}