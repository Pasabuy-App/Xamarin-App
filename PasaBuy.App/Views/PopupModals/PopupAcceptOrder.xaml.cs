using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAcceptOrder : PopupPage
    {
        public static string store_logo;
        public static string carItem = "car / sedan";
        public static string item_id = "#-154145";// string.Empty;
        public static string storeName = "PasaBuy Dev Store"; //string.Empty;
        public static string store_lat = "14.357342"; // string.Empty;
        public static string store_long = "121.058751"; // string.Empty;
        public static string user_lat = "14.3291744";// string.Empty;
        public static string user_long = "121.0063577"; // string.Empty;
        public static string orderName = string.Empty;
        public static string waypointAddress = "National Road San Galing, San Pedro Laguna, Philippines";// string.Empty;
        public static string destinationAddress = "BLock 10 Lot 18 Narra St. San Francisco, Biñan Laguna, Philippines"; // string.Empty;
        public static string orderTime = "08:30 AM"; //string.Empty;


        Stopwatch stopwatch = new Stopwatch();

        public PopupAcceptOrder()
        {
            InitializeComponent();

            Store.Text = storeName;
            Order.Text = item_id;// + " | " + orderTime;
            WaypointAddress.Text = waypointAddress;
            OriginAddress.Text = destinationAddress;
            OrderTime.Text = "30";
            OrderTimer(true);
            DashboardPage.time = false;// remove this if you want to remove the timer
        }

        public void OrderTimer(Boolean flag)
        {
            int TimeLimit = 30;
            if (flag == true)
            {
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    int countdown = TimeLimit - stopwatch.Elapsed.Seconds;
                    OrderTime.Text = countdown.ToString();
                    if (countdown == 1)
                    {
                        DashboardPage.time = true;// remove this if you want to remove the timer
                        DashboardPage.PushOrder("");
                        PopupNavigation.Instance.PopAsync();
                        return false;
                    }
                    return true;
                });

            }
            else
            {
                stopwatch.Stop();
            }
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
            DashboardPage.time = true;// remove this if you want to remove the timer
            DashboardPage.PushOrder("");
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
            DashboardPage.time = true;// remove this if you want to remove the timer
            DashboardPage.PushOrder("");
            return base.OnBackButtonPressed();
        }

        async private void AcceptOrder(object sender, EventArgs e)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            StartDeliveryPage.item_id = item_id;
            StartDeliveryPage.storeName = storeName;
            StartDeliveryPage.orderName = orderName;
            StartDeliveryPage.orderTime = orderTime;
            StartDeliveryPage.waypointAddress = waypointAddress;
            StartDeliveryPage.destinationAddress = destinationAddress;

            StartDeliveryPage.StoreLatittude = store_lat;
            StartDeliveryPage.StoreLongitude = store_long;

            StartDeliveryPage.UserLatitude = user_lat;
            StartDeliveryPage.userLongitude = user_long;

            PopupNavigation.Instance.PopAsync();
            await Navigation.PushModalAsync(new StartDeliveryPage());
            OrderTimer(false);
            DashboardPage.time = false;// remove this if you want to remove the timer
            DashboardPage._OrderList.Add( new Models.eCommerce.Transactions() { ID = item_id }); // Add orderid to observable collection.
            StartDeliveryPage.order_status = "preparing";
        }
    }
}