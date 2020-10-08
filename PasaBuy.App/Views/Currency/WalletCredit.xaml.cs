using PasaBuy.App.Models.Currency;
using PasaBuy.App.ViewModels.Currency;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletCredit"/> class.
        /// </summary>
        public WalletCredit()
        {
            InitializeComponent();
            LastIndex = 11;
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
    }
}