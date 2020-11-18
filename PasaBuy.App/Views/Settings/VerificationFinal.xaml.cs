using PasaBuy.App.Controllers.Notice;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerificationFinal : ContentPage
    {
        public VerificationFinal()
        {
            InitializeComponent();
        }

        private void BackToHome(object sender, EventArgs e)
        {
            Continue();
        }

        protected override bool OnBackButtonPressed()
        {
            Continue();
            return base.OnBackButtonPressed();
        }

        private async void Continue()
        {
            //await Application.Current.MainPage.DisplayAlert(
            //    "asdsads", "Ikaw na ay isang verified user ng PasaBuy.App! Makakatanggap ka ng Php300 sa E-wallet mo na magagamit mo sa PasaBuy Plus Promo. Maghintay lang ng updates mula sa amin upang malaman kung kailan niyo magagamit ang app.", "Continue", "");
             App.Current.MainPage = new MainTabs();
        }
    }
}