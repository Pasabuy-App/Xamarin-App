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
    public partial class WalletSaving : ContentView
    {
        public static int LastIndex = 11;
        public bool isTapped;
        public static string currency_id;
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletSaving"/> class.
        /// </summary>
        public WalletSaving()
        {
            InitializeComponent();
            LastIndex = 11;
            isTapped = false;
        }

        private void SfListView_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as WalletSavingsModel;
            if (WalletSavingViewModel._SavingsList.Last() == item && WalletSavingViewModel._SavingsList.Count() != 1)
            {
                if (WalletSavingViewModel._SavingsList.IndexOf(item) >= LastIndex)
                {
                    WalletSavingViewModel.LoadData(WalletSavingViewModel.currency_id, (LastIndex += 1).ToString());
                    LastIndex += 6;
                }
            }
        }

        private async void SendMoney_Tapped(object sender, EventArgs e)
        {
            //await SendImage.FadeTo(0.3, 200);
            //await SendImage.FadeTo(1, 200);
            //Views.Currency.SendWalletSavings.currency_id = currency_id;
            if (!isTapped)
            {
                isTapped = true;
                await App.Current.MainPage.Navigation.PushModalAsync(new SendWalletSavings());
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
                await PopupNavigation.Instance.PushAsync(new PopupShowWalletSavings());
                isTapped = false;
            }
        }
    }
}