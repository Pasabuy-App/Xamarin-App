using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAcceptOrder : PopupPage
    {
        public static string carItem = "car / sedan";
        public static string item_id = string.Empty;
        public static string storeName = string.Empty;
        public static string store_lat = string.Empty;
        public static string store_long = string.Empty;
        public static string user_lat = string.Empty;
        public static string user_long = string.Empty;
        public static string orderName = string.Empty;
        public static string waypointAddress = string.Empty;
        public static string destinationAddress = string.Empty;
        public static string orderTime = string.Empty;

        Stopwatch stopwatch = new Stopwatch();

        public PopupAcceptOrder()
        {
            InitializeComponent();

            Order.Text = item_id + " | "+orderTime;
            WaypointAddress.Text = waypointAddress;
            OriginAddress.Text = destinationAddress;
            OrderTime.Text = "30";
            OrderTimer(true);

        }

        public void OrderTimer(Boolean flag)
        {
            int TimeLimit = 30;
            stopwatch.Start();
            if (flag == true)
            {
                
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    OrderTime.Text = (TimeLimit - stopwatch.Elapsed.Seconds).ToString();
                    if (30 - stopwatch.Elapsed.Seconds == 1)
                    {
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
        }

        async private void AcceptOrder(object sender, EventArgs e)
        {

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            StartDeliveryPage.item_id = item_id;
            StartDeliveryPage.storeName = storeName;
            StartDeliveryPage.waypointAddress = waypointAddress;
            StartDeliveryPage.destinationAddress = destinationAddress;
            new Alert("", destinationAddress, "ok");
            
            StartDeliveryPage.StoreLatittude = Convert.ToDouble(store_lat);
            StartDeliveryPage.StoreLongitude = Convert.ToDouble(store_long);

            StartDeliveryPage.UserLatitude = Convert.ToDouble(user_lat);
            StartDeliveryPage.userLongitude = Convert.ToDouble(user_long);


            await Navigation.PushModalAsync(new StartDeliveryPage());
            await PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
            return;
            HatidPress.Deliveries.Instance.Accept(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "100", "item_id", "car / sedan", location.Latitude.ToString(), location.Longitude.ToString(), (bool success, string data) => 
            {
                try
                {

                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                    Console.WriteLine("Handle not supported on device exception" + " " + fnsEx);
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                    Console.WriteLine("Handle not enabled on device exception" + " " + fneEx);
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    Console.WriteLine("Handle permission exception" + " " + pEx);
                }
                catch (Exception ex)
                {
                    // Unable to get location
                    Console.WriteLine("Unable to get location" + " " + ex);
                }
            });
           new Alert("Ok", "Add command and bind this to viewmodel", "ok");
        }
    }
}