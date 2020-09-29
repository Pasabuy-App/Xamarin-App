using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Position = Xamarin.Forms.Maps.Position;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.DataService;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Driver;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Navigation : ContentPage
    {
        public static string StoreAddress = string.Empty;
        public static string UserAddress = string.Empty;
        public static Position StorePosition;
        public static Position UserPosition;
        

        ILocationUpdateService loc;
        public Navigation()
        {
            InitializeComponent();
                DisplayCurloc();
           
          
            /*customMap.IsShowingUser = true;
            NavigationViewModel.DisplayCurloc(customMap);*/
        }


        public async void DisplayCurloc()
        {
            try
            {
                Geocoder geoCoder = new Geocoder();
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.444));
                    customMap.MoveToRegion(mapSpan);

                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(p);
                    string address = possibleAddresses.FirstOrDefault();

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    new Alert("",StoreAddress,"ok");
                    if (StoreAddress == string.Empty)
                    {
                        customMap.Pins.Clear();
                    }


                    if (StoreAddress != string.Empty)
                    {
                        Console.WriteLine("", "user pos " + UserAddress + " User position" + UserPosition + ", \n Store Position" + StoreAddress + " position" + StorePosition, "ok");
                        //new Alert("","user pos "+UserAddress+" User position"+UserPosition+ ", \n Store Position"+StoreAddress+" position"+StorePosition,"ok");
                        CustomPin pin1 = new CustomPin
                        {
                            Type = PinType.Place,
                            Position = UserPosition,
                            Label = "Client",
                            Address = UserAddress,
                            Name = "",
                            Url = ""
                        };
                        customMap.CustomPins = new List<CustomPin> { pin1 };
                        customMap.Pins.Add(pin1);

                        CustomPin pin2 = new CustomPin
                        {
                            Type = PinType.Place,
                            Position = StorePosition,
                            Label = "Store",
                            Address = StoreAddress,
                            Name = "",
                            Url = ""
                        };
                        customMap.CustomPins = new List<CustomPin> { pin2 };
                        customMap.Pins.Add(pin2);

                        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(p, Distance.FromMiles(2)));
                    }


                    /* CustomPin pin = new CustomPin
                     {
                         Type = PinType.Place,
                         Position = new Position(location.Latitude, location.Longitude),
                         Label = "Your Location",
                         Address = address.ToString(),
                         Name = "Rider",
                         Url = "http://xamarin.com/about/"
                     };
                     customMap.CustomPins = new List<CustomPin> { pin };*/

                    //customMap.Pins.Add(pin);
                    if (StoreAddress == string.Empty)
                    { 
                        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(0.111)));

                    }

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

      

        public static void FetchPin(Position pos1, Position pos2, string add1, string add2, string label1, string label2)
        {
            CustomMap map = new CustomMap();
            try
            {
                CustomPin pin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = pos1,
                    Label = label1,
                    Address = add1,
                    Name = "",
                    Url = "http://xamarin.com/about/"
                };
                map.CustomPins = new List<CustomPin> { pin };

                map.Pins.Add(pin);

                CustomPin pins = new CustomPin
                {
                    Type = PinType.Place,
                    Position = pos2,
                    Label = label2,
                    Address = add2,
                    Name = "",
                    Url = "http://xamarin.com/about/"
                };
                map.CustomPins = new List<CustomPin> { pins };
                map.Pins.Add(pins);
            }
            catch (Exception e)
            {
                new Alert("ok", e.ToString(), "ok");
            }
        }

    }
}