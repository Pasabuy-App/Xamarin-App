using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();

            foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
            {
                if (per.action == "edit_profile")
                {
                    isEditProfile.IsVisible = true;
                    isEditProfileBar.IsVisible = true;
                    break;
                }
                else
                {
                    isEditProfile.IsVisible = false;
                    isEditProfileBar.IsVisible = false;
                }
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsAddressView());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsProfileView());
        }
    }
}