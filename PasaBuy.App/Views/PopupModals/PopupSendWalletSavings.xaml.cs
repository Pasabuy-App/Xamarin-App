using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Currency;
using PasaBuy.App.ViewModels.Currency;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupSendWalletSavings : PopupPage
    {
        public static string currency_id;
        public static string walletid;
        public static string amount;
        public static string notes;
        public static string type;

        public PopupSendWalletSavings()
        {
            InitializeComponent();
            Verify(walletid, amount);
            //this.BindingContext = new WalletSavingViewModel();
        }

        public void Verify(string walletid, string amount)
        {
            try
            {
                CoinPress.Wallet.Instance.Verify(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, walletid, (bool success, string data) =>
                {
                    if (success)
                    {
                        WalletSavingsModel wallet = JsonConvert.DeserializeObject<WalletSavingsModel>(data);
                        for (int i = 0; i < wallet.data.Length; i++)
                        {
                            Message.Text = "Do you really want to send ₱" + amount + " to " + wallet.data[i].name + " with wallet ID of " + walletid + "?";
                            Recipient.Text = wallet.data[i].name;
                            ImageId.Source = PSAProc.GetUrl(wallet.data[i].avatar);
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        private void CancelModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Console.WriteLine("wallet id: " + walletid + " amount: " + amount + " currency: " + currency_id);
                CoinPress.Wallet.Instance.Send(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, walletid, amount, currency_id, notes, (bool success, string data) =>
                {
                    if (success)
                    {
                        if (type == "savings")
                        {
                            WalletSavingViewModel._SavingsList.Clear();
                            WalletSavingViewModel.LoadData(WalletSavingViewModel.currency_id, "");
                            Currency.WalletSaving.LastIndex = 11;
                        }
                        if (type == "credits")
                        {
                            WalletCreditViewModel._CreditsList.Clear();
                            WalletCreditViewModel.LoadData(WalletCreditViewModel.currency_id, "");
                            Currency.WalletCredit.LastIndex = 11;
                        }
                        new Alert("Send Money", "Send money successfully.", "OK");
                        PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + ex, "OK");
            }
        }
    }
}