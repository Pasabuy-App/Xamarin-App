using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasabayPage : ContentPage
    {
        public bool isBtn = false;
        public PasabayPage()
        {
            InitializeComponent();
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Loader.IsVisible = true;
                Loader.IsRunning = true;
                if (!isBtn)
                {
                    isBtn = true;
                    Destinations.HasError = string.IsNullOrEmpty(Destination.Text) || string.IsNullOrWhiteSpace(Destination.Text) ? true : false;
                    ReturnPlaces.HasError = string.IsNullOrEmpty(ReturnPlace.Text) || string.IsNullOrWhiteSpace(ReturnPlace.Text) ? true : false;
                    if (Destinations.HasError == false && ReturnPlaces.HasError == false)
                    {
                        Http.SocioPress.Post.Instance.Insert("Pasabay", "content", "pasabay", "", "", TimePicker.Time.ToString(), Destination.Text, ReturnPlace.Text, DatePicker.Date.ToString(), (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
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
                                Navigation.PopModalAsync();
                                isBtn = false;
                                Loader.IsVisible = false;
                                Loader.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                Loader.IsVisible = false;
                                Loader.IsRunning = false;
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                isBtn = false;
                Loader.IsVisible = false;
                Loader.IsRunning = false;
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: SPV1PST-I1PSBP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1PST-I1PSBP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-I1PSBP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1PST-I1PSBP-" + err.ToString());
                }
            }
        }
    }
}