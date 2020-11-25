using PasaBuy.App.Local;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFillPage : ContentPage
    {
        public static string idType = string.Empty;
        public static string idnumber = string.Empty;
        public static string idDocType = string.Empty;
        public static string idPath = string.Empty;
        public static string selfiePath = string.Empty;
        public static string instructions = string.Empty;
        private bool isEnable = false;
        public VerificationFillPage()
        {
            InitializeComponent();
            Loader.IsVisible = false;
            IdTypeEntry.Text = idType;
            IDNumberEntry.Text = idnumber;
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void NextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(IdTypeEntry.Text) || String.IsNullOrEmpty(IDNumberEntry.Text) || String.IsNullOrEmpty(ContactEntry.Text))
                {
                    new Controllers.Notice.Alert("Failed", "Please complete all fields.", "Ok");
                }
                else
                {
                    if (!isEnable)
                    {
                        Loader.IsVisible = true;
                        isEnable = true;
                        Http.DataVice.Documents.Instance.Insert("id", instructions, "face", idDocType, ContactEntry.Text,  idnumber, selfiePath, idPath, (bool success, string data) =>
                        {
                            if (success)
                            {
                                Navigation.PushModalAsync(new VerificationFinal());
                                Loader.IsVisible = false;
                                isEnable = false;
                            }
                            else
                            {
                                isEnable = false;
                                Loader.IsVisible = false;
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                    }
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1DOC-I1VFP", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1DOC-I1VFP-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1DOC-I1VFP.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1DOC-I1VFP-" + err.ToString());
                }
                isEnable = false;
                Loader.IsVisible = false;
            }
        }
    }
}