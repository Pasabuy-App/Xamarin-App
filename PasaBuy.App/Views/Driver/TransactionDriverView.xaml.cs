using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using PasaBuy.App.Models.Driver;
using Xamarin.Essentials;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionDriverView : ContentPage
    {
        public TransactionDriverView()
        {
            InitializeComponent();
            this.BindingContext = new PendingOrderViewModel();

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



        private async void TapGestureRecognizer_Tapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            /*  var btn = (Label)sender;
              var id = btn.ClassId;*/
            var smp = e.ItemData as TransactListData;
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            Views.PopupModals.PopupAcceptOrder.store_logo = smp.Store_logo;
            Views.PopupModals.PopupAcceptOrder.item_id = smp.Hash_id;
            Views.PopupModals.PopupAcceptOrder.storeName = smp.Store;
            Views.PopupModals.PopupAcceptOrder.waypointAddress = smp.Store_address;
            Views.PopupModals.PopupAcceptOrder.destinationAddress = smp.Customer_address;

            Views.PopupModals.PopupAcceptOrder.orderTime = smp.Date_created;
            Views.PopupModals.PopupAcceptOrder.orderName = smp.Product;
            Views.PopupModals.PopupAcceptOrder.store_lat = smp.Store_lat;
            Views.PopupModals.PopupAcceptOrder.store_long = smp.Store_long;
            Views.PopupModals.PopupAcceptOrder.user_lat = smp.Customer_lat;
            Views.PopupModals.PopupAcceptOrder.user_long = smp.Customer_long;

            await PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
         
        }
    }
}