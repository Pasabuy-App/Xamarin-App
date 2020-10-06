using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.eCommerce
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressInMapPage : ContentPage
    {
        public static string street;
        public static string address_id;
        public static double lat;
        public static double lon;


        public AddressInMapPage()
        {
            InitializeComponent();
            StreetEntry.Text = street + " lan: " + lat.ToString() + " lon: " + lon.ToString() + " id: " + address_id;

            Pin pin1 = new Pin()
            {
                Type = PinType.Place,
                Label = street,
                Address = address_id,
                Position = new Position(lat, lon),
                Rotation = 0,
                IsDraggable = true,
                Tag = "id -toawd",
            };
            map.Pins.Add(pin1);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(23)));
        }

        async void map_PinDragEnd(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

            lat = e.Pin.Position.Latitude;
            lon = e.Pin.Position.Longitude;

            latitude.Text = lat.ToString();
            longitude.Text = lon.ToString();

            //await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude :" + e.Pin.Position.Longitude, "Ok");
        }
    }
}