using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasaBuy.App.Models.Navigation;
using PasaBuy.App.Services;
using PasaBuy.App.Local;
using Xamarin.Essentials;
using MobilePOS;
using PasaBuy.App.Models.Driver;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Controllers.Notice;
using Xamarin.Forms.GoogleMaps;

namespace PasaBuy.App.ViewModels
{
    public class MapPageViewModel
    {
        public MapPageViewModel() 
        { 
        }

        public class VehicleLocations
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        internal async Task<List<VehicleLocations>> LoadVehicles()
        {
            //Call the api to get the vehicles nearby

            //This are the hardcoded data
            List<VehicleLocations> vehicleLocations = new List<VehicleLocations>
            {
                new VehicleLocations{Latitude = 28.128888,Longitude=82.296891},
                new VehicleLocations{Latitude = 28.130061,Longitude=82.297364},
                new VehicleLocations{Latitude = 28.129550,Longitude=82.298887},
                new VehicleLocations{Latitude = 28.127336,Longitude=82.292106},

            };
            return vehicleLocations;
        }


        public static void Get_order(string itemId)
        {
            try
            {
                 Order.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", "", (bool success, string data) =>
                 {
                    //new Alert("", data, "ok");
                    if (success)
                    {
                        OrderListData datas = JsonConvert.DeserializeObject<OrderListData>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string costumer_address = datas.data[i].customer_address;
                            string store_address = datas.data[i].store_address;
                            double user_lat = datas.data[i].customer_lat;
                            double user_long = datas.data[i].customer_long;
                            double store_lat = datas.data[i].store_lat;
                            double store_long = datas.data[i].store_long;
                            /*  Position s_p = new Position(store_lat, store_long);
                              Position c_p = new Position(user_lat, user_long);*/
                            //new Alert("ok", " " + store_address, "ok");
                            /*  Views.Driver.Navigation.StoreAddress = store_address;
                              Views.Driver.Navigation.UserAddress = costumer_address;
                              Views.Driver.Navigation.StorePosition = s_p;
                              Views.Driver.Navigation.UserPosition = c_p;
  */
                        }
                        PopupNavigation.Instance.PushAsync(new PopupAcceptOrder());
                    }
                    else
                    {
                        new Alert("err", "err", "ok");
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



        internal async Task<System.Collections.Generic.List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute(
            Position position, 
            string destinationLatitude1, 
            string destinationLongitude1, 
            string mode,
            string waypointLattitude,
            string waypointLongitude)
        {
            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections(
                $"{position.Latitude}", 
                $"{position.Longitude}", 
                $"{destinationLatitude1}", 
                $"{destinationLongitude1}", 
                $"{mode}",
                $"{waypointLattitude}",
                $"{waypointLongitude}");

            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                return positions;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Add your payment method inside the Google Maps console.", "Ok");
                return null;

            }

        }

    }
}
