﻿
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Onboarding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerifyAccountPage : ContentPage
    {
        public VerifyAccountPage()
        {
            InitializeComponent();
        }

        private void backButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}