using PasaBuy.App.Views.Onboarding;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupConfirmLogout : PopupPage
    {
        public PopupConfirmLogout()
        {
            InitializeComponent();
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void ConfirmLogout(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(false);
            Application.Current.MainPage = new NavigationPage(new SignInPage());
            Preferences.Remove("UserInfo");

        }
    }
}