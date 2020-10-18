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
                if (!isBtn)
                {
                    isBtn = true;
                    Destinations.HasError = string.IsNullOrEmpty(Destination.Text) || string.IsNullOrWhiteSpace(Destination.Text) ? true : false;
                    ReturnPlaces.HasError = string.IsNullOrEmpty(ReturnPlace.Text) || string.IsNullOrWhiteSpace(ReturnPlace.Text) ? true : false;
                    if (Destinations.HasError == false && ReturnPlaces.HasError == false)
                    {
                        SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "Pasabay", "content", "pasabay", "", "", TimePicker.Time.ToString(), Destination.Text, ReturnPlace.Text, DatePicker.Date.ToString(), (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton == string.Empty)
                                {
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
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                isBtn = false;
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
                isBtn = false;
            }
        }
    }
}