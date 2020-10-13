using DataVice;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePWPage : ContentPage
    {
        private bool isEnable = false;
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
            try
            {
                if (String.IsNullOrEmpty(OldPassword.Text) || String.IsNullOrEmpty(NewPassword.Text) || String.IsNullOrEmpty(ConfirmNewPassword.Text))
                {
                    new Alert("Failed", "Please complete all fields.", "Ok");
                }
                else
                {
                    if (!isEnable)
                    {
                        isEnable = true;
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Delay(1000);
                            isEnable = false;
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