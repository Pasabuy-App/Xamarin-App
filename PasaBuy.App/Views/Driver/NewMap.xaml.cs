using PasaBuy.App.DataService;
using PasaBuy.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMap : ContentPage
    {
        ILocationUpdateService loc;
        MapPageViewModel mapPageViewModel;

        public double curlocLatitude = 0;
        public double curlocLongitude = 0;


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

        public NewMap()
        {
            InitializeComponent();
            BindingContext = mapPageViewModel = new MapPageViewModel();

            Pin pins = new Pin()
            {
                Type = PinType.Place,
                Label = "tyawd",
                Address = "awd",
                Position = new Position(35.71d, 139.81d),
                Rotation = 0,
                Tag = "id -toawd",
            };
            map.Pins.Add(pins);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pins.Position, Distance.FromKilometers(5000)));
        }

        /* public async void DisplayCurloc()
         {
             try
             {
                 var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                 var location = await Geolocation.GetLocationAsync(request);

                 if (location != null)
                 {
                     Position p = new Position(location.Latitude, location.Longitude);
                     MapSpan mapSpan = MapSpan.FromCenterAndRadius(p, Distance.FromKilometers(.444));
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
             map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

         }
         // Update Vehicle
         void Button_Clicked_1(System.Object sender, System.EventArgs e)
         {
             var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
             map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

             Device.StartTimer(TimeSpan.FromSeconds(5), () => TimerStarted());

         }

         private bool TimerStarted()
         {
             Device.BeginInvokeOnMainThread(async () =>
             {
                 Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
                 Compass.ReadingChanged += Compass_ReadingChanged;
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
                         map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)), Distance.FromMiles(0.1)));
                         //Position ps = new Position(item.Latitude, item.Longitude);
                         //UpdatePostions(ps);
                          var cPin = map.Pins.FirstOrDefault();

                          if (cPin != null)
                          {
                              cPin.Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));
                              //cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 13, HeightRequest = 13 });
                              map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMiles(0.1)));
                              var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                              map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                          }



                     }
                 }
             }

             );
             Compass.Stop();
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
             map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
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
             map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
         }

         #endregion

         async void TrackPath_Clicked(System.Object sender, System.EventArgs e)
         {
             map.Polylines.Clear();
             Device.StartTimer(TimeSpan.FromSeconds(1), () => TimerStarted());


             var request = new GeolocationRequest(GeolocationAccuracy.Medium);
             var location = await Geolocation.GetLocationAsync(request);
             Position pos = new Position(curlocLatitude, curlocLongitude);

             var pathcontent = await mapPageViewModel.LoadRoute(pos);


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

             map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromKilometers(3)));
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

             var positionIndex = 1;

 */




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
        //}

        /* async void UpdatePostions(Xamarin.Forms.GoogleMaps.Position position)
         {
             if (map.Pins.Count == 1 && map.Polylines != null && map.Polylines?.Count > 1)
                 return;

             var cPin = map.Pins.FirstOrDefault();

             if (cPin != null)
             {
                 cPin.Position = new Xamarin.Forms.GoogleMaps.Position(position.Latitude, position.Longitude);
                 //cPin.Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 13, HeightRequest = 13 });
                 map.MoveToRegion(MapSpan.FromCenterAndRadius(cPin.Position, Distance.FromMiles(0.1)));
                 var previousPosition = map.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                 map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
             }
             else
             {
                 map.Polylines?.FirstOrDefault()?.Positions?.Clear();
             }
         }*/
    }
}