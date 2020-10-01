using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using PasaBuy.App.iOS;
using PasaBuy.App.DataService;

[assembly: Dependency(typeof(ILocationUpdateService))]
namespace PasaBuy.App.iOS
{
    public class LocationEventArgs : ILocationEventArgs
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class LocationUpdateServices : ILocationUpdateService
    {
        CLLocationManager locationManager;

        public event EventHandler<ILocationEventArgs> LocationChanged;

        event EventHandler<ILocationEventArgs> ILocationUpdateService.LocationChanged
        {
            add
            {
                LocationChanged += value;
            }
            remove
            {
                LocationChanged -= value;
            }
        }

        public void GetUserLocation()
        {
            locationManager = new CLLocationManager
            {
                DesiredAccuracy = CLLocation.AccuracyBest,
                DistanceFilter = CLLocationDistance.FilterNone
            };
            locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
            {
                var locations = e.Locations;
                LocationEventArgs args = new LocationEventArgs
                {
                    Latitude = locations[locations.Length - 1].Coordinate.Latitude,
                    Longitude = locations[locations.Length - 1].Coordinate.Longitude
                };
                LocationChanged(this, args);
            };
            locationManager.AuthorizationChanged += (object sender, CLAuthorizationChangedEventArgs e) =>
            {
                if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
                {
                    locationManager.StartUpdatingLocation();
                }
            };
            locationManager.RequestWhenInUseAuthorization();
        }
        ~LocationUpdateServices()
        {
            locationManager.StopUpdatingLocation();
        }
    }

}