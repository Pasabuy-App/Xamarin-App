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
            App.Current.MainPage = new MainTabs();
        }
        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage = new MainTabs();
            return base.OnBackButtonPressed();
        }
    }
}