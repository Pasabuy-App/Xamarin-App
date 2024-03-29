﻿using System;
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
        public static string full_address;
        public static string type;
        public static string contact;
        public static string person;
        public static double lat;
        public static double lon;


        public  AddressInMapPage()
        {
            InitializeComponent();
            StreetEntry.Text = street;// + " lat: " + lat.ToString() + " lon: " + lon.ToString() + " id: " + address_id;
            
            if (lat == 0 || lon == 0)
            {
                Pin pin1 = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Drag pin to Choose address",
                    Address = full_address,
                    Position = new Position(14.5680867, 120.8805591),
                    Rotation = 0,
                    IsDraggable = true,
                    Tag = "id -toawd",
                };
                map.Pins.Add(pin1);
                CameraPosition camera = new CameraPosition(pin1.Position, 0.1, 2.0, 5.0);
                map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(camera));
                //map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(pin1.Position, 10.23));
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(3)));
            }
            else
            {

                Pin pin1 = new Pin()
                {
                    Type = PinType.Place,
                    Label = "Your address",
                    Address = full_address,
                    Position = new Position(lat, lon),
                    Rotation = 0,
                    Icon = BitmapDescriptorFactory.DefaultMarker(Color.FromHex("#f7b900")),
                    IsDraggable = true,
                    Tag = "id -toawd",
                };
                map.Pins.Add(pin1);
                //map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(Ro));
                CameraPosition camera = new CameraPosition(pin1.Position, 0.1, 2.0, 5.0);
                map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(camera));
                //map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(pin1.Position, 10.23));
                map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(0.1)));
            }

        }

        async void map_PinDragEnd(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);//Latitude, Longitude
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500))); 
            //await map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(positions, 19.23));
            
            lat = e.Pin.Position.Latitude;
            lon = e.Pin.Position.Longitude;

            await map.AnimateCamera(CameraUpdateFactory.NewPositionZoom(positions, 18.23));
           

            //await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude :" + e.Pin.Position.Longitude, "Ok");
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void map_CameraMoving(object sender, CameraMovingEventArgs e)
        {
        }
    }
}