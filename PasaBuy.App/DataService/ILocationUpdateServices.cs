using System;

namespace PasaBuy.App.DataService
{
    public interface ILocationUpdateService
    {
        void GetUserLocation();
        event EventHandler<ILocationEventArgs> LocationChanged;
    }
    public interface ILocationEventArgs
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
