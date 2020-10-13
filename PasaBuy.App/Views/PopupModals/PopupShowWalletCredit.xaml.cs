using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupShowWalletCredit : PopupPage
    {
        public PopupShowWalletCredit()
        {
            InitializeComponent();
            WalletID.Text = ViewModels.Currency.WalletCreditViewModel.wallet_id;
        }

        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(WalletID.Text);
            if (Clipboard.HasText)
            {
                await PopupNavigation.Instance.PopAsync();
                var text = await Clipboard.GetTextAsync();
                await DisplayAlert("Success", string.Format("Copied to clipboard {0}.", text), "OK");
            }
        }
    }
}