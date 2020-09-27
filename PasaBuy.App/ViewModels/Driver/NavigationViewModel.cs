using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Position = Xamarin.Forms.Maps.Position;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.DataService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.ViewModels.Driver
{
    public class NavigationViewModel : BaseViewModel
    {
        public NavigationViewModel()
        {
          /*  CustomMap mp = new CustomMap();
            this.MapNames = mp;
            DisplayCurloc(mp);*/
        }

        public string mapname;

        public string MapNames
        {
            get
            {
                return mapname;
            }
            set
            {
                if (mapname != value)
                {
                    mapname = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public async static void DisplayCurloc(CustomMap map)
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
                    map.MoveToRegion(mapSpan);

                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(p);
                    string address = possibleAddresses.FirstOrDefault();

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                   CustomPin pin = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(location.Latitude, location.Longitude),
                        Label = "Your Location",
                        Address = address.ToString(),
                        Name = "Rider",
                        Url = "http://xamarin.com/about/"
                    };
                    map.CustomPins = new List<CustomPin> { pin };

                    map.Pins.Add(pin);

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(0.111)));

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




        

    }
}
