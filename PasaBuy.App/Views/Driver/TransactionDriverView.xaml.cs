using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDriverView : ContentPage
    {
        public TransactionDriverView()
        {
            InitializeComponent();
        }

        private void ShowAcceptOrder(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void NewOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
        }
    }
}