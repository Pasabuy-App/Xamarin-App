using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (!isBtn)
                {
                    isBtn = true;
                    Descriptions.HasError = string.IsNullOrEmpty(Description.Text) || string.IsNullOrWhiteSpace(Description.Text) ? true : false;
                    ItemNames.HasError = string.IsNullOrEmpty(ItemName.Text) || string.IsNullOrWhiteSpace(ItemName.Text) ? true : false;
                    Locations.HasError = string.IsNullOrEmpty(Location.Text) || string.IsNullOrWhiteSpace(Location.Text) ? true : false;
                    if (Descriptions.HasError == false && ItemNames.HasError == false && Locations.HasError == false)
                    {
                        SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, ItemName.Text, Description.Text, "pabili", "", "", TimePicker.Time.ToString(), Location.Text, "", DatePicker.Date.ToString(), (bool success, string data) =>
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
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Delay(100);
                            isBtn = false;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}