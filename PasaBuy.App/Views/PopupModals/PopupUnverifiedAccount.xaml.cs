using PasaBuy.App.Views.Onboarding;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupUnverifiedAccount : PopupPage
    {
        public PopupUnverifiedAccount()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void VerifyButton(object sender, EventArgs e)
        {
            Verify.IsEnabled = false;
            Navigation.PushModalAsync(new VerifyAccountPage());
            PopupNavigation.Instance.PopAsync();
            Verify.IsEnabled = true;

        }
    }
}