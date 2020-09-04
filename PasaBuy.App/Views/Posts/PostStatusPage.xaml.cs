using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Feeds;

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
            SocioPress.Posts.Instance.Insert(UserPrefs.Instance.UserInfo.wpid, UserPrefs.Instance.UserInfo.snky, "Title", StatusEditor.Text, "status", (bool success, string data) =>
            {
                if (success)
                {
                    Navigation.PopModalAsync();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });
        }


    }
}