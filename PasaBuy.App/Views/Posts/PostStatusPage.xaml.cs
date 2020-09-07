using System;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Feeds;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Posts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class PostStatusPage : ContentPage
    {
        public PostStatusPage()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public void SubmitPostButton(object sender, EventArgs e)
        {
            InsertPost();
        }
        public void InsertPost()
        {
            SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "Title", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    ProfileGetData.CountPost(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky);
                    Navigation.PopModalAsync();
                    HomepageViewModel.RefreshList();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }


    }
}