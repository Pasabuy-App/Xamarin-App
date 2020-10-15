using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Buttons;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendWalletSavings : ContentPage
    {
        public static string currency_id;
        public SendWalletSavings()
        {
            InitializeComponent();

            ViewModels.Currency.WalletSavingViewModel._SavingsList.CollectionChanged += CollectionChanges;
        }

        private async void CollectionChanges(object sender, EventArgs e)
        {
            await Task.Delay(100);
            WalletId.Text = "";
            Amount.Text = "";
            Note.Text = "";
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void ShowModal(object sender, EventArgs e)
        {
            WalletIds.HasError = !string.IsNullOrWhiteSpace(WalletId.Text) ? false : true;
            Amounts.HasError = !string.IsNullOrWhiteSpace(Amount.Text) ? false : true;
            var btn = sender as SfButton;
            if (btn.IsEnabled)
            {
                btn.IsEnabled = false;
                if (!WalletIds.HasError && !Amounts.HasError)
                {
                    PopupSendWalletSavings.currency_id = currency_id;
                    PopupSendWalletSavings.amount = Amount.Text;
                    PopupSendWalletSavings.walletid = WalletId.Text;
                    PopupSendWalletSavings.notes = Note.Text;
                    PopupSendWalletSavings.type = "savings";
                    //Console.WriteLine("Example: " + Note.Text + Amount.Text + WalletId.Text);
                    await PopupNavigation.Instance.PushAsync(new PopupSendWalletSavings());
                }
                await Task.Delay(200);
                btn.IsEnabled = true;
            }
        }
    }
}