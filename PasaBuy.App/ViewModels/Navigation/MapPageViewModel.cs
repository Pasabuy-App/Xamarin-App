using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasaBuy.App.Models.Navigation;
using PasaBuy.App.Local;
using Xamarin.Essentials;
using MobilePOS;
using PasaBuy.App.Models.Driver;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Services;
using Xamarin.Forms.GoogleMaps;
using PasaBuy.App.DataService;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels
{
    public class MapPageViewModel
    {
        ILocationUpdateService loc;

        public double curlocLat = 0;
        public double curlocLong = 0;

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

            loc = DependencyService.Get<ILocationUpdateService>();
            loc.LocationChanged += (object sender, ILocationEventArgs args) =>
            {
                curlocLat = args.Latitude;
                curlocLong = args.Longitude;
            };
            loc.GetUserLocation();

            //Call the api to get the vehicles nearby

            //This are the hardcoded data
            List<VehicleLocations> vehicleLocations = new List<VehicleLocations>
            {
                new VehicleLocations{Latitude = curlocLat,Longitude=curlocLong},

            };
            return vehicleLocations;
        }

        internal async Task<System.Collections.Generic.List<Xamarin.Forms.GoogleMaps.Position>> LoadRoute(Position pos, string destinationLatitude, string destinationLongitude1, string mode, string waypointlatitude, string waypointlongitude)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            var googleDirection = await ApiServices.ServiceClientInstance.GetDirections($"{pos.Latitude}", $"{pos.Longitude}", $"{destinationLatitude}", $"{destinationLongitude1}", $"{mode}", $"{waypointlatitude}", $"{waypointlongitude}");
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
