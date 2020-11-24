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
            Loader.IsRunning = true;
            Loader.IsVisible = true;
            try
            {
                if (String.IsNullOrEmpty(OldPassword.Text) || String.IsNullOrEmpty(NewPassword.Text) || String.IsNullOrEmpty(ConfirmNewPassword.Text))
                {
                    new Alert("Failed", "Please complete all fields.", "Ok");
                    Loader.IsRunning = false;
                    Loader.IsVisible = false;
                }
                else
                {
                    if (!isEnable)
                    {
                        isEnable = true;
                       
                        Http.DataVice.Users.Instance.ChangePassword(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, OldPassword.Text, NewPassword.Text, ConfirmNewPassword.Text, (bool success, string data) =>
                        {
                            if (success)
                            {
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                                App.Current.MainPage.DisplayAlert("Success", "Password changed successfully!", "Ok");
                                Navigation.PopModalAsync();
                                
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                Loader.IsRunning = false;
                                Loader.IsVisible = false;
                            }
                        });
                        isEnable = false;
                        Loader.IsRunning = false;
                        Loader.IsVisible = false;

                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1URS-CP1CPWP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1URS-CP1CPWP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-CP1CPWP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1URS-CP1CPWP-" + err.ToString());
                }
                Loader.IsRunning = false;
            }
        }
    }
}