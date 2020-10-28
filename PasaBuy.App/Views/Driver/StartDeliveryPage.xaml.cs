using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.DataService;
using PasaBuy.App.Local;
using PasaBuy.App.ViewModels;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Reflection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartDeliveryPage : ContentPage
    {
        #region Fields

        public static string status_deli = "Slide to right to Update Status";
        public string deliveryStatus;
        public static string carItem = string.Empty;
        public static string item_id = string.Empty;
        public static string storeName = string.Empty;
        public static string orderName = string.Empty;
        public static string waypointAddress = string.Empty;
        public static string destinationAddress = string.Empty;
        public static string orderTime = string.Empty;

        public static string StoreLatittude = string.Empty;
        public static string StoreLongitude = string.Empty;
        public static string UserLatitude = string.Empty;
        public static string userLongitude = string.Empty;
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


        public StartDeliveryPage()
        {
            map = new Xamarin.Forms.GoogleMaps.Map();
            InitializeComponent();
            BindingContext = mapPageViewModel = new MapPageViewModel();
            status.Text = status_deli;

            pinLocation();
            DisplayCurloc();
            StoreName.Text = storeName;
            StoreAddress.Text = waypointAddress;
            ClientAddress.Text = destinationAddress;

            //OrderIDandOrderTime.Text = item_id;

            /*switch (deliveryStatus)
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
            }*/


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
                    MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(2));
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


        public async void StartDelivery_Clicked(System.Object sender, System.EventArgs e)
        {
            Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
            Compass.ReadingChanged += Compass_ReadingChanged;

            //StartDelivery.Text = "Ongoing";
            PopupNavigation.Instance.PushAsync(new PopupOpenExternalMap());

            map.Polylines.Clear();
            Device.StartTimer(TimeSpan.FromSeconds(1), () => TimerStarted());


            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            Position pos = new Position(curlocLatitude, curlocLongitude);

            var pathcontent = await mapPageViewModel.LoadRoute(pos, UserLatitude.ToString(), userLongitude.ToString(), "driving", StoreLatittude.ToString(), StoreLongitude.ToString());

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

        }


        #region page Method
        async void Handle_SlideCompleted(object sender, System.EventArgs e)
        {
            switch (stats.Text)
            {
                case "I've recieve the package":

                    stats.Text = "Deliver Package";

                    break;

                case "Deliver Package":

                    stats.Text = "I've Delivered the package";

                    if (Device.RuntimePlatform == Device.iOS)
                    {

                        var location2 = new Location(Convert.ToDouble(UserLatitude), Convert.ToDouble(userLongitude));

                        await Xamarin.Essentials.Map.OpenAsync(location2);
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {

                        var location2 = new Location(Convert.ToDouble(UserLatitude), Convert.ToDouble(userLongitude));

                        await Xamarin.Essentials.Map.OpenAsync(location2);

                    }

                    break;
                case "I've Delivered the package":
                    new Alert("", item_id, "ok");
                    HatidPress.Deliveries.Instance.Complete_Cancel(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, item_id, "completed", (bool success, string data) =>
                    {
                        try
                        {
                            if (success)
                            {
                                stats.Text = "For Pickup";

                                Navigation.PushModalAsync(new DashboardPage());
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
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
                    });


                    break;

            }

            if (stats.Text == "For Pickup")
            {
                stats.Text = "I've recieve the package";


                if (Device.RuntimePlatform == Device.iOS)
                {

                    var location = new Location(Convert.ToDouble(StoreLatittude), Convert.ToDouble(StoreLongitude));
                    var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

                    await Xamarin.Essentials.Map.OpenAsync(location);
                }
                else if (Device.RuntimePlatform == Device.Android)
                {

                    var location = new Location(Convert.ToDouble(StoreLatittude), Convert.ToDouble(StoreLongitude));
                    var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

                    await Xamarin.Essentials.Map.OpenAsync(location);

                }

            }

        }

        public void pinLocation()
        {
            Pin pin1 = new Pin()
            {
                Type = PinType.Place,
                Label = storeName,
                Address = waypointAddress,
                Position = new Position(Convert.ToDouble(StoreLatittude), Convert.ToDouble(StoreLongitude)),
                Rotation = 0,
                Tag = "id -toawd",
            };
            map.Pins.Add(pin1);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin1.Position, Xamarin.Forms.GoogleMaps.Distance.FromKilometers(2)));

            Pin pin2 = new Pin()
            {
                Type = PinType.Place,
                Label = "Client",
                Address = destinationAddress,
                Position = new Position(Convert.ToDouble(UserLatitude), Convert.ToDouble(userLongitude)),
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
        protected override bool OnBackButtonPressed()
        {
            Compass.Stop();
            Navigation.PopModalAsync();
            return base.OnBackButtonPressed();
        }

        public static async void OpenMapApp()
        {

        }
    }
}