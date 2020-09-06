using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.Views.Feeds;
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
            SocioPress.Posts.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "Title", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    Navigation.PopModalAsync();
                    //HomepageViewModel.RefreshData();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }


    }
}