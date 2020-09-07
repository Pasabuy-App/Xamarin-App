using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Views.Master;
using PasaBuy.App.Controllers.Notice;
using DataVice;
using PasaBuy.App.Local;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePWPage : ContentPage
    {
        public ChangePWPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        /// <summary>
        /// Invoked when save button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //new Alert("Demoguy Notice", "Saving of new user password is not yet implemented. Thank you for your patience!", "AGREE");
            Users.Instance.ChangePassword(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, OldPassword.Text, NewPassword.Text, ConfirmNewPassword.Text, (bool success, string data) =>
            {
                if (success)
                {
                    Navigation.PopModalAsync();
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                }
            });
        }
    }
}