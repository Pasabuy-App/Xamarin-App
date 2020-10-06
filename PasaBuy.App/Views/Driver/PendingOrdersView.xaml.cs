using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.ViewModels.Driver;
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
    public partial class PendingOrdersView : ContentView
    {
        public PendingOrdersView()
        {
            InitializeComponent();
            this.BindingContext = new AcceptedOrderViewModel();
            


        }

        private void PendingOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var smp = e.ItemData as AcceptedListOrder;
            StartDeliveryPage.StoreLatittude = smp.WaypointLat.ToString();
            StartDeliveryPage.StoreLongitude = smp.WaypointLong.ToString();
            StartDeliveryPage.userLongitude = smp.DestinationLong.ToString();
            StartDeliveryPage.UserLatitude = smp.DestinationLat.ToString();
            StartDeliveryPage.waypointAddress = smp.WaypointAddress;
            StartDeliveryPage.destinationAddress = smp.DestinationAddress;
           
            Navigation.PushModalAsync(new StartDeliveryPage());
        }
    }
}