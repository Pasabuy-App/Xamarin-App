using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.DataService;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Navigation;
using PasaBuy.App.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace PasaBuy.App.ViewModels
{
    public class MapPageViewModel : BaseViewModel
    {
        ILocationUpdateService loc;

        public double curlocLat = 0;
        public double curlocLong = 0;

        public Command CompleteCommand { get; set; }
        public Command CancelCommand { get; set; }

        public bool _isVisible = false;
        public bool isVisible 
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public MapPageViewModel()
        {
            this.CompleteCommand = new Command(this.CompleteClicked);
            this.CancelCommand = new Command(this.CancelClicked);

            IsBusy = false;
            UpdateStatus(Views.Driver.StartDeliveryPage.item_id);

        }


        public void UpdateStatus(string odid)
        {

            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    Http.MobilePOS.Order.Instance.Listing("", odid, "", "", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.MobilePOS.OrderDetailsModel order = JsonConvert.DeserializeObject<Models.MobilePOS.OrderDetailsModel>(data);
                            for (int i = 0; i < order.data.Length; i++)
                            {
                                Views.Driver.StartDeliveryPage.order_status = order.data[i].stages;
                                if (order.data[i].stages == "Shipping")
                                {
                                    isVisible = true;
                                }
                                else
                                {
                                    isVisible = false;
                                }
                            }
                            IsBusy = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsBusy = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1MPVM.", "OK");
                IsBusy = false;
            }
        }
        public async void Popup()
        {
            Views.Driver.DashboardPage._OrderList.Clear();
            App.Current.MainPage.Navigation.PopModalAsync();
            await PopupNavigation.PushAsync(new Views.PopupModals.PopupRateDriver());
        }

        private void CompleteClicked(object obj)
        {
            UpdateOrderStatus(Views.Driver.StartDeliveryPage.item_id, "completed");
            //Popup();
        }

        private void CancelClicked(object obj)
        {
            UpdateOrderStatus(Views.Driver.StartDeliveryPage.item_id, "cancelled");
        }

        public async void UpdateOrderStatus(string odid, string status)
        {
            try
            {
                string stats = status == "completed" ? "complete" : "cancel";
                bool answer =  await Application.Current.MainPage.DisplayAlert("Notice to Mover", "Are you sure to " + stats + " this?", "Yes", "No");
                if (answer)
                {
                    if (!IsBusy)
                    {
                        IsBusy = true;
                        Http.MobilePOS.Order.Instance.UpdateStages(odid, status, "", async (bool success, string data) =>
                        {
                            if (success)
                            {
                                Views.Driver.DashboardPage._OrderList.Clear();
                                await Application.Current.MainPage.Navigation.PopModalAsync();
                                IsBusy = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsBusy = false;
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-U1MPVM.", "OK");
                IsBusy = false;
            }
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
