using PasaBuy.App.ViewModels.MobilePOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPersonnelView : ContentPage
    {
        public AddPersonnelView()
        {
            InitializeComponent();
            this.BindingContext = new AddPersonnelViewModel();
            SearchEntry.Completed += (sender, args) => SearchStore(sender, args);
            AddPersonnelViewModel._userList.CollectionChanged += CollectionChanges;
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
                    AddPersonnelViewModel.SearchUser(SearchEntry.Text);
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
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