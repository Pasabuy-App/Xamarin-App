using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasaBuy.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using PasaBuy.App.DataService;
using System.Reflection;
using Rg.Plugins.Popup.Services;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartDeliveryPage : ContentPage
    {
        #region Fields
     

        public string deliveryStatus; 
        public static string carItem = string.Empty;
        public static string item_id = string.Empty;
        public static string storeName = string.Empty;
        public static string orderName = string.Empty;
        public static string waypointAddress = string.Empty;
        public static string destinationAddress = string.Empty;
        public static string orderTime = string.Empty;

        public static double StoreLatittude = 0;
        public static double StoreLongitude = 0;
        public static double UserLatitude = 0;
        public static double userLongitude = 0;
        public static bool drawpath = false;
        public static double curlocLatitude = 0;
        public static double curlocLongitude = 0;

        
        #endregion


        ILocationUpdateService loc;
        MapPageViewModel mapPageViewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loc = DependencyService.Get<ILocationUpdateService>();
            loc.LocationChanged += (object sender, ILocationEventArgs args) =>
            {
                curlocLatitude = args.Latitude;
                curlocLongitude = args.Longitude;
            };
            loc.GetUserLocation();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            loc = null;
        }


        public  StartDeliveryPage()
        {
            map =  new Xamarin.Forms.GoogleMaps.Map();
            InitializeComponent();
            BindingContext = mapPageViewModel = new MapPageViewModel();

            pinLocation();
            DisplayCurloc();
            StoreName.Text = storeName;
            StoreAddress.Text = waypointAddress;
            ClientAddress.Text = destinationAddress;
            //OrderIDandOrderTime.Text = item_id;

            switch (deliveryStatus)
            {
                case "true":
                    StartDelivery.Text = "Ongoing";
                    break;
                case "ongoing":
                    StartDelivery.Text = "Continue";
                    break;
                default:
                    StartDelivery.Text = "Start Delivery";
                    break;
            }


        }


        public async void DisplayCurloc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(.944));
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


        private void ApplyMapTheme()
        {
            var assembly = typeof(NewMap).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"polyline.Theme.json");
            Console.WriteLine(stream + "theme");
            string themeFile;
            using (var reader = new System.IO.StreamReader(stream))
            {

                themeFile = reader.ReadToEnd();
            }
            map.MapStyle = MapStyle.FromJson(themeFile);
        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var contents = await mapPageViewModel.LoadVehicles();

            if (contents != null)
            {
                foreach (var item in contents)
                {
                    Pin VehiclePins = new Pin()
                    {
                        Label = "Cars",
                        Type = PinType.Place,
                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                        Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),

                    };
                    map.Pins.Add(VehiclePins);
                }
            }

            //This is your location and it should be near to your car location.
            var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Xamarin.Forms.GoogleMaps.Distance.FromMeters(500)));

        }
        // Update Vehicle
        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Xamarin.Forms.GoogleMaps.Distance.FromMeters(500)));

            Device.StartTimer(TimeSpan.FromSeconds(5), () => TimerStarted());

        }

        private bool TimerStarted()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                Position pos = new Position(curlocLatitude, curlocLongitude);

   
                map.Pins.Clear();
                //map.Polylines.Clear();
                //Get the cars nearrby from api but here we are hardcoding the contents
                var contents = await mapPageViewModel.LoadVehicles();
                var positionIndex = 1;

                if (contents != null)
                {
                    foreach (var item in contents)
                    {
                        var smp = positionIndex + 1;
                        Pin VehiclePins = new Pin()
                        {
                            Label = "Cars",
                            Type = PinType.Place,
                            Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 10, HeightRequest = 10 }),
                            Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),
                            Rotation = ToRotationPoints(headernothvalue)
                        };
                        map.Pins.Add(VehiclePins);
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.1)));
                        
                        //Position ps = new Position(item.Latitude, item.Longitude);
                        //UpdatePostions(ps);*/
                        /* var cPin = map.Pins.FirstOrDefault();

                         if (cPin != null)
                         {
                             cPin.Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));
                             //cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 13, HeightRequest = 13 });
                             map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMiles(0.1)));
                             var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                             map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                         }
 */


                    }
                }
            }

            );
            //Compass.Stop();
            return true;
        }

        private float ToRotationPoints(double headernothvalue)
        {
            return (float)headernothvalue;

        }

        double headernothvalue;
        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            headernothvalue = data.HeadingMagneticNorth;
        }

        #region for pin drag
        void map_PinDragStart(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {


        }

        async void map_PinDragEnd(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Xamarin.Forms.GoogleMaps.Distance.FromMeters(500)));
            await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude :" + e.Pin.Position.Longitude, "Ok");
        }

        void PickupButton_Clicked(System.Object sender, System.EventArgs e)
        {
            //User Actual Location
            Pin VehiclePins = new Pin()
            {
                Label = "Xamarin Guy",
                Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("PickupPin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "PickupPin.png", WidthRequest = 30, HeightRequest = 30 }),
                Position = new Position(Convert.ToDouble("28.126751"), Convert.ToDouble("82.297092")),
                IsDraggable = true

            };
            map.Pins.Add(VehiclePins);
            //This is my actual location as of now we are taking it from google maps. But you have to use location plugin to generate latitude and longitude.
            var positions = new Position(28.126751, 82.297092);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Xamarin.Forms.GoogleMaps.Distance.FromMeters(500)));
        }

        #endregion

       /* void StartDelivery_Clicked(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupOpenExternalMap());
        }*/


        public  async void StartDelivery_Clicked(System.Object sender, System.EventArgs e)
        {
            Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
            Compass.ReadingChanged += Compass_ReadingChanged;

            StartDelivery.Text = "Ongoing";
            PopupNavigation.Instance.PushAsync(new PopupOpenExternalMap());

            map.Polylines.Clear();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => TimerStarted());


            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            Position pos = new Position(curlocLatitude, curlocLongitude);

            var pathcontent = await mapPageViewModel.LoadRoute(pos, UserLatitude.ToString(), userLongitude.ToString(), "driving", StoreLatittude.ToString(), StoreLongitude.ToString());


            //var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            //var stream = assembly.GetManifestResourceStream($"XamGMaps.MapResources.TrackPath.json");
            //string trackPathFile;

            //using (var reader = new System.IO.StreamReader(stream))
            //{
            //    trackPathFile = reader.ReadToEnd();
            //}

            //var myJson = JsonConvert.DeserializeObject<List<Xamarin.Forms.GoogleMaps.Position>>(trackPathFile);



            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.DarkOrange;
            polyline.StrokeWidth = 3;

            foreach (var p in pathcontent)
            {
                polyline.Positions.Add(p);

            }
            map.Polylines.Add(polyline);

            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromKilometers(3)));
            string cars = "CircleImg.png";
            var pin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.SearchResult,
                Position = new Xamarin.Forms.GoogleMaps.Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "Pin",
                Address = "Pin",
                Tag = "CirclePoint",
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle(cars) : BitmapDescriptorFactory.FromView(new Image() { Source = cars, WidthRequest = 5, HeightRequest = 5 })

            };
            map.Pins.Add(pin);



            /*  Device.StartTimer(TimeSpan.FromSeconds(1), () =>
              {
                  if (pathcontent.Count > positionIndex)
                  {
                      UpdatePostions(pos);
                       //positionIndex++;
                       return true;
                  }
                  else
                  {
                      return false;
                  }
              });*/
        }
/*
        async void UpdatePostions(Xamarin.Forms.GoogleMaps.Position position)
        {
            if (map.Pins.Count == 1 && map.Polylines != null && map.Polylines?.Count > 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                //cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 13, HeightRequest = 13 });
                map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.1)));
                var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }
        }

*/

        #region page Method
        /* async void StartDelivery_Clicked(object sender, EventArgs e)
         {


         }*/

        public  void pinLocation()
        {
            Pin pin1 = new Pin()
            {
                Type = PinType.Place,
                Label = storeName,
                Address = waypointAddress,
                Position = new Position(StoreLatittude, StoreLongitude),
                Rotation = 0,
                Tag = "id -toawd",
            };
            map.Pins.Add(pin1);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(23)));

            Pin pin2 = new Pin()
            {
                Type = PinType.Place,
                Label = "Client",
                Address = destinationAddress,
                Position = new Position(UserLatitude, userLongitude),
                Rotation = 0,
                Tag = "id -toawd",
            };
            map.Pins.Add(pin2);
            //ap.MoveToRegion(MapSpan.FromCenterAndRadius(pin2.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(1000)));

        }
        #endregion

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Compass.Stop();
            Navigation.PopModalAsync();
        }

        public static async void OpenMapApp()
        {
            String s = waypointAddress;
            s = s.Replace(" ", "+");

            if (Device.RuntimePlatform == Device.iOS)
            {
                
                // https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                await Launcher.OpenAsync("http://maps.apple.com/?daddr=San+Francisco,+CA&saddr=cupertino");
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                // opens the 'task chooser' so the user can pick Maps, Chrome or other mapping app
                //await Launcher.OpenAsync("http://www.google.com/maps/dir/Bytes+Crafter,+B10+L18+Narra+St,+Silcas+Village,+Brgy.+San+Francisco,+Bi%C3%B1an,+4024+Laguna/14.3312268,121.0653564/Metro+San+Jose+Public+Market/@14.3249305,121.0479903,15z/data=!4m15!4m14!1m5!1m1!1s0x3397d7accf8e5839:0xbfdc9ef48149ab86!2m2!1d121.0413718!2d14.3291764!1m0!1m5!1m1!1s0x3397d71e2367fd5b:0xc40629c1d70faf69!2m2!1d121.0341372!2d14.3305162!3e0");
                var main_url = "http://www.google.com/maps/dir/";
          
                await Launcher.OpenAsync(main_url+$"{curlocLatitude},{curlocLongitude}/{UserLatitude},{userLongitude}/{s}@{StoreLatittude},{StoreLongitude},15z");
                //await Launcher.OpenAsync("https://www.google.com/maps/dir/,121.0434482/14.3205938,121.0481447/Metro+San+Jose+Public+Market,+General+Mariano+Alvarez,+4117+Cavite/@14.3278519,121.0464376,15.25z/data=!4m10!4m9!1m0!1m0!1m5!1m1!1s0x3397d71e2367fd5b:0xc40629c1d70faf69!2m2!1d121.0341372!2d14.3305162!3e0");
            }
        }
    }
}