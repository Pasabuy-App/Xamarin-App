using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            DisplayCurloc();
            map.IsTrafficEnabled = true;

            fetch_order(0);

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

        private void ShowAvailableDeliveries(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new TransactionDriverView());
        }

        private async void fetch_order(int smp)
        {

            try
            {
                HatidPress.Deliveries.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", "accepted", (bool success, string data) =>
                {
                    if (success)
                    {
                        TransactListData datas = JsonConvert.DeserializeObject<TransactListData>(data);

                        if (datas.data.Length != 0)
                        {
                            Continue_deliver.IsVisible = true;
                            Pending_Order.IsVisible = false;

                            if (smp == 1)
                            {

                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    string Store_logo = datas.data[i].store_logo;
                                    string ID = datas.data[i].id;
                                    string ItemID = datas.data[i].hash_id;
                                    string Price = datas.data[i].price;
                                    string Product = datas.data[i].product_name;
                                    string Quantity = datas.data[i].quantity;
                                    string Status = datas.data[i].status;
                                    string Store = datas.data[i].store_name;
                                    string Date_created = datas.data[i].date_created;
                                    string Date_ordered = datas.data[i].date_ordered;
                                    string store_lat = datas.data[i].store_lat;
                                    string store_long = datas.data[i].store_long;
                                    string store_address = datas.data[i].store_address;
                                    string customer_lat = datas.data[i].customer_lat;
                                    string customer_long = datas.data[i].customer_long;
                                    string customer_address = datas.data[i].customer_address;

                                    StartDeliveryPage.item_id = ItemID;
                                    StartDeliveryPage.storeName = Store;
                                    StartDeliveryPage.waypointAddress = store_address;
                                    StartDeliveryPage.destinationAddress = customer_address;

                                    StartDeliveryPage.StoreLatittude = store_lat;
                                    StartDeliveryPage.StoreLongitude = store_long;

                                    StartDeliveryPage.UserLatitude = customer_lat;
                                    StartDeliveryPage.userLongitude = customer_long;
                                    StartDeliveryPage.status_deli = "I've recieve the package";
                                    Navigation.PushModalAsync(new StartDeliveryPage());

                                }
                            }
                        }
                        else
                        {
                            Pending_Order.IsVisible = false;
                            Continue_deliver.IsVisible = false;
                        }
                    }
                });
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

        private void Continue_delivery_Tapped(object sender, EventArgs e)
        {
            fetch_order(1);
        }
    }
}