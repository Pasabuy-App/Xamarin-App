using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
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