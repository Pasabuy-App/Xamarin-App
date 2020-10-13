using PasaBuy.App.DataService;
using PasaBuy.App.Models.Navigation;
using PasaBuy.App.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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
