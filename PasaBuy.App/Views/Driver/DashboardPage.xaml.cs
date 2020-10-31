using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.PopupModals;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System.Linq;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.MobilePOS;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public static bool time;
        public static System.Collections.ObjectModel.ObservableCollection<Models.eCommerce.Transactions> _OrderList;
        public System.Collections.ObjectModel.ObservableCollection<Models.eCommerce.Transactions> OrderList
        {
            get
            {
                return _OrderList;
            }
            set
            {
                _OrderList = value;
                this.OnPropertyChanged(nameof(OrderList));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        public DashboardPage()
        {
            InitializeComponent();
            DisplayCurloc();
            map.IsTrafficEnabled = true;
            //fetch_order(0);
            time = true;
            PushOrder("");
            Pending_Order.IsVisible = false;
            _OrderList = new System.Collections.ObjectModel.ObservableCollection<Models.eCommerce.Transactions>();
            _OrderList.CollectionChanged += CollectionChages;
        }

        public static void PushOrder(string odid)
        {
            GetOrderDetails("qlnv1bb");
            Device.StartTimer(TimeSpan.FromSeconds(15), doitt);
            bool doitt()
            {
                if (time)
                {
                    PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void GetOrderDetails(string odid)
        {
            try
            {
                Http.MobilePOS.Order.Instance.Listing(odid, async (bool success, string data) =>
                {
                    if (success)
                    {
                        OrderDetailsModel order = JsonConvert.DeserializeObject<OrderDetailsModel>(data);
                        for (int i = 0; i < order.data.Length; i++)
                        {
                            PopupAcceptOrder.store_logo = order.data[i].store_logo == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(order.data[i].store_logo);
                            PopupAcceptOrder.storeName = order.data[i].store_name;
                            PopupAcceptOrder.store_lat = order.data[i].store_lat;
                            PopupAcceptOrder.store_long = order.data[i].store_long;
                            PopupAcceptOrder.waypointAddress = order.data[i].store_address;

                            PopupAcceptOrder.item_id = order.data[i].pubkey;
                            PopupAcceptOrder.orderName = "#" + order.data[i].pubkey;
                            PopupAcceptOrder.destinationAddress = order.data[i].cutomer_address;
                            PopupAcceptOrder.user_lat = order.data[i].cutomer_lat;
                            PopupAcceptOrder.user_long = order.data[i].cutomer_long;
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

        private void CollectionChages(object sender, EventArgs e)
        {
            if (_OrderList.Count > 0)
            {
                Pending_Order.IsVisible = true; // Show the Ongoing deliveries button.
                // If the order is accepted by the mover, add the item to observable collection.
            }
            else
            {
                time = true;
                PushOrder("");
                Pending_Order.IsVisible = false; // Hide the Ongoing deliveries button.
                // If the status of the order is shipped or completed or cancelled, clear the observable collection.
            }
        }

        // Display Current Location of User
        public async void DisplayCurloc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Xamarin.Forms.GoogleMaps.Position p = new Xamarin.Forms.GoogleMaps.Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.444));
                    map.MoveToRegion(mapSpan);
                    //await GetLocationName(p);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
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
        }

        private async void ShowAvailableDeliveries(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new StartDeliveryPage());
        }
    }
}