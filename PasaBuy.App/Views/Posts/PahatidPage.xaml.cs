using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PahatidPage : ContentPage
    {
        public bool isBtn = false;
        public PahatidPage()
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
                    PickUps.HasError = string.IsNullOrEmpty(PickUp.Text) || string.IsNullOrWhiteSpace(PickUp.Text) ? true : false;
                    DropOffs.HasError = string.IsNullOrEmpty(DropOff.Text) || string.IsNullOrWhiteSpace(DropOff.Text) ? true : false;
                    if (PickUps.HasError == false && DropOffs.HasError == false)
                    {
                        SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "Pahatid", "content", "pahatid", "", "", TimePicker.Time.ToString(), PickUp.Text, DropOff.Text, DatePicker.Date.ToString(), (bool success, string data) =>
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