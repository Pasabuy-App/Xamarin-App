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
using System.Threading.Tasks;

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
        public static string order_id;
        public static string stages;
        public DashboardPage()
        {
            InitializeComponent();
            DisplayCurloc();
            map.IsTrafficEnabled = true;
            CheckDeliveries();

            _OrderList = new System.Collections.ObjectModel.ObservableCollection<Models.eCommerce.Transactions>();
            _OrderList.CollectionChanged += CollectionChages;
        }

        private void CollectionChages(object sender, EventArgs e)
        {
            Refresh.IsEnabled = true;
            Pending_Order.IsVisible = false;
        }

        public void CheckDeliveries()
        {
            try
            {
                Http.HatidPress.Order.Instance.Verify( (bool success, string data) =>
                {
                    if (success)
                    {
                        OrderDetailsModel order = JsonConvert.DeserializeObject<OrderDetailsModel>(data);
                        for (int i = 0; i < order.data.Length; i++)
                        {
                            GetOrderDetails(order.data[i].order_id, 0);
                            order_id = order.data[i].order_id;
                            Refresh.IsEnabled = false;
                            Pending_Order.IsVisible = true;
                        }
                    }
                    else
                    {
                        Refresh.IsEnabled = true;
                        Pending_Order.IsVisible = false;
                    }
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2ODR-V1DP.", "OK");
                Refresh.IsEnabled = true;
                Pending_Order.IsVisible = false;
            }
            //Check if have ongoing deliveries then set to visible the pending order and  set to false the visibility of refresh. ongoing, preparing and shipping status
            //If not, set the visiblity of pending order to false then set the visiblity of refresh to true.
        }

        public static void GetOrderDetails(string odid, int timer)
        {
            try
            {
                Http.MobilePOS.Order.Instance.Listing("", odid, "", async (bool success, string data) =>
                {
                    if (success)
                    {
                        OrderDetailsModel order = JsonConvert.DeserializeObject<OrderDetailsModel>(data);
                        if (timer > 0)
                        {
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
                                PopupAcceptOrder.countdown = timer;
                            }
                            await Task.Delay(500);
                            PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
                        }
                        else
                        {
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                StartDeliveryPage.item_id = order.data[i].pubkey;
                                StartDeliveryPage.storeName = order.data[i].store_name;
                                StartDeliveryPage.orderName = "#" + order.data[i].pubkey;
                                StartDeliveryPage.waypointAddress = order.data[i].store_address;
                                StartDeliveryPage.destinationAddress = order.data[i].cutomer_address;

                                StartDeliveryPage.StoreLatittude = order.data[i].store_lat;
                                StartDeliveryPage.StoreLongitude = order.data[i].store_long;

                                StartDeliveryPage.UserLatitude = order.data[i].cutomer_lat;
                                StartDeliveryPage.userLongitude = order.data[i].cutomer_long;
                                StartDeliveryPage.order_status = order.data[i].stages;
                            }
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

        private void RefreshClicked(object sender, EventArgs e)
        {
            try
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    Http.HatidPress.Order.Instance.Queuing((bool success, string data) =>
                    {
                        if (success)
                        {
                            OrderDetailsModel order = JsonConvert.DeserializeObject<OrderDetailsModel>(data);
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                GetOrderDetails(order.data[i].order_id, Convert.ToInt32(order.data[i].countdown));
                            }
                            Refresh.IsEnabled = false;
                            Pending_Order.IsVisible = true;
                            IsRunning.IsRunning = false;
                        }
                        else
                        {
                            Refresh.IsEnabled = true;
                            Pending_Order.IsVisible = false;
                            IsRunning.IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2ODR-Q1DP.", "OK");
                IsRunning.IsRunning = false;
            }
        }
    }
}