using PasaBuy.App.Models.Currency;
using PasaBuy.App.ViewModels.Currency;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    /// <summary>
    /// My wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletCredit : ContentView
    {
        public static int LastIndex = 11;
        public bool isTapped;
        public static string currency_id;
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletCredit"/> class.
        /// </summary>
        public WalletCredit()
        {
            InitializeComponent();
            LastIndex = 11;
            isTapped = false;
        }

        private void SfListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as WalletCreditsModel;
            if (WalletCreditViewModel._CreditsList.Last() == item && WalletCreditViewModel._CreditsList.Count() != 1)
            {
                if (WalletCreditViewModel._CreditsList.IndexOf(item) >= LastIndex)
                {
                    WalletCreditViewModel.LoadData(WalletCreditViewModel.currency_id, (LastIndex += 1).ToString());
                    LastIndex += 6;
                }
            }
        }

        private async void SendMoney_Tapped(object sender, EventArgs e)
        {
            //await SendImage.FadeTo(0.3, 200);
            //await SendImage.FadeTo(1, 200);
            //Views.Currency.SendWalletCredits.currency_id = currency_id;
            if (!isTapped)
            {
                isTapped = true;
                await App.Current.MainPage.Navigation.PushModalAsync(new SendWalletCredits());
            isTapped = false;
            }
        }

        private async void ReceiveMoney_Tapped(object sender, EventArgs e)
        {
            //await ReceiveImage.FadeTo(0.3, 200);
            //await ReceiveImage.FadeTo(1, 200);
            if (!isTapped)
            {
                isTapped = true;
                await PopupNavigation.Instance.PushAsync(new PopupShowWalletCredit());
                isTapped = false;
            }
        }
    }
}