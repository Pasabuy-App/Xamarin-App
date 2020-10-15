using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupShowWalletSavings : PopupPage
    {
        public PopupShowWalletSavings()
        {
            InitializeComponent();
            WalletID.Text = ViewModels.Currency.WalletSavingViewModel.wallet_id;
        }
        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void CopyID_Clicked(object sender, EventArgs e)
        {
           await Clipboard.SetTextAsync(WalletID.Text);
           if (Clipboard.HasText)
           {
                var text = await Clipboard.GetTextAsync();
                //await DisplayAlert("Success", string.Format("Copied to clipboard {0}.", text), "OK");
                Plugin.Toast.CrossToastPopUp.Current.ShowToastMessage(string.Format("Copied to clipboard {0}.", text));
                await PopupNavigation.Instance.PopAsync();
           }
        }
    }
}