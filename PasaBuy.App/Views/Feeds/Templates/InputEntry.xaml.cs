using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.Posts;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Feeds.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputEntry : ContentView
    {
        public bool isTapped;
        public InputEntry()
        {
            InitializeComponent();

            PostEntry.Completed += (sender, args) => SubmitPostButton(sender, args);
            isTapped = false;
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PostEntry.Text))
            {
                try
                {
                    Http.SocioPress.Post.Instance.Insert(PostEntry.Text, "", "status", "", "", "", "", "", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                            {
                                Views.Feeds.HomePage.LastIndex = 12;
                                Views.Feeds.HomePage.isFirstLoad = false;
                                HomepageViewModel.homePostList.Clear();
                                HomepageViewModel.LoadData("");
                            }
                            else
                            {
                                MyProfileViewModel.profilePostList.Clear();
                                MyProfileViewModel.LoadData(PSACache.Instance.UserInfo.wpid);
                            }
                            PostEntry.Text = string.Empty;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception ex)
                {
                    new Alert("Something went Wrong", "Please contact administrator.", "OK");
                }
            }
            else
            {
                new Alert("Notice to user", "Please fill-up all fields.", "OK");
            }
        }


        public async void PostStatus(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostStatusPage());
                isTapped = false;
            }
        }

        public async void PostRequest(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                //Navigation.PushModalAsync(new PostRequestPage());
                await PopupNavigation.Instance.PushAsync(new PopupPasabuy());
                isTapped = false;
            }
        }


        public async void PostSell(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostSellPage());
                isTapped = false;
            }
        }

        public async void AddStatusPost(object sender, EventArgs args)
        {
            if (!isTapped)
            {
                isTapped = true;
                await Navigation.PushModalAsync(new PostStatusPage());
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








    }
}