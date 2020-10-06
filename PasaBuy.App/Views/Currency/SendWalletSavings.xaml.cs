using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Currency
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendWalletSavings : ContentPage
    {
        public SendWalletSavings()
        {
            InitializeComponent();
        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void ShowModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupSendWalletSavings());
        }
    }
}