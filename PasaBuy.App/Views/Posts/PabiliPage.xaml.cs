using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PabiliPage : ContentPage
    {
        public bool isBtn = false;
        public PabiliPage()
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
                Loader.IsRunning = true;
                Loader.IsVisible = true;

                if (!isBtn)
                {
                    isBtn = true;
                    Descriptions.HasError = string.IsNullOrEmpty(Description.Text) || string.IsNullOrWhiteSpace(Description.Text) ? true : false;
                    ItemNames.HasError = string.IsNullOrEmpty(ItemName.Text) || string.IsNullOrWhiteSpace(ItemName.Text) ? true : false;
                    Locations.HasError = string.IsNullOrEmpty(Location.Text) || string.IsNullOrWhiteSpace(Location.Text) ? true : false;
                    if (Descriptions.HasError == false && ItemNames.HasError == false && Locations.HasError == false)
                    {
                        Http.SocioPress.Post.Instance.Insert(ItemName.Text, Description.Text, "pabili", "", "", TimePicker.Time.ToString(), Location.Text, "", DatePicker.Date.ToString(), (bool success, string data) =>
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
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: SPV1PST-I1PBLP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1PST-I1PBLP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-I1PBLP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1PST-I1PBLP-" + err.ToString());
                }
                isBtn = false;
                Loader.IsRunning = false;
                Loader.IsVisible = false;
            }
        }
    }
}