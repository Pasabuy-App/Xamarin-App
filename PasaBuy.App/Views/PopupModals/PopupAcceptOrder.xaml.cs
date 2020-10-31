using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Library;
using PasaBuy.App.Local;
using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.PopupModals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupAcceptOrder : PopupPage
    {
        public static string store_logo;
        public static string carItem;//= "car / sedan";
        public static string item_id;//= "#-154145";// string.Empty;
        public static string storeName;//= "PasaBuy Dev Store"; //string.Empty;
        public static string store_lat;//= "14.357342"; // string.Empty;
        public static string store_long;//= "121.058751"; // string.Empty;
        public static string user_lat;// = "14.3291744";// string.Empty;
        public static string user_long;// = "121.0063577"; // string.Empty;
        public static string orderName;//= string.Empty;
        public static string waypointAddress;// = "National Road San Galing, San Pedro Laguna, Philippines";// string.Empty;
        public static string destinationAddress;// = "BLock 10 Lot 18 Narra St. San Francisco, Biñan Laguna, Philippines"; // string.Empty;
        public static string orderTime;//= "08:30 AM"; //string.Empty;


        Stopwatch stopwatch = new Stopwatch();

        public PopupAcceptOrder()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
            Store.Text = storeName;
            Order.Text = orderName;// + " | " + orderTime;
            WaypointAddress.Text = waypointAddress;
            OriginAddress.Text = destinationAddress;
            OrderTime.Text = "30";
            OrderTimer(true);
            DashboardPage.time = false;// remove this if you want to remove the timer
        }

        public void OrderTimer(Boolean flag)
        {
            int TimeLimit = 30;
            if (flag == true)
            {
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    int countdown = TimeLimit - stopwatch.Elapsed.Seconds;
                    OrderTime.Text = countdown.ToString();
                    if (countdown == 1)
                    {
                        DashboardPage.time = true;// remove this if you want to remove the timer
                        DashboardPage.PushOrder("");
                        PopupNavigation.Instance.PopAsync();
                        return false;
                    }
                    return true;
                });

            }
            else
            {
                stopwatch.Stop();
            }
        }

        private void CloseModal(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
            DashboardPage.time = true;// remove this if you want to remove the timer
            DashboardPage.PushOrder("");
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
            DashboardPage.time = true;// remove this if you want to remove the timer
            DashboardPage.PushOrder("");
            return base.OnBackButtonPressed();
        }

        private void AcceptOrder(object sender, EventArgs e)
        {
            Accept_Order(item_id);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isGpsEnable;

        public bool IsGpsEnable
        {
            get
            {
                return _isGpsEnable;
            }
            set
            {
                _isGpsEnable = value;
                this.NotifyPropertyChanged();
            }
        }

        public async void Accept_Order(string odid)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
            }
            catch (FeatureNotEnabledException featureNotEnabledException)
            {
                await Application.Current.MainPage.DisplayAlert("Notice to User", "Please enable your location first.", "OK");
                Xamarin.Forms.DependencyService.Get<IGpsDependencyService>().OpenSettings();

            }

            IsGpsEnable = Xamarin.Forms.DependencyService.Get<IGpsDependencyService>().IsGpsEnable();

            if (IsGpsEnable)
            {
                try
                {
                    if (!IsRunning.IsRunning)
                    {
                        Http.HatidPress.Order.Instance.Accept_Order(odid, async (bool success, string data) =>
                        {
                            if (success)
                            {

                                StartDeliveryPage.item_id = item_id;
                                StartDeliveryPage.storeName = storeName;
                                StartDeliveryPage.orderName = orderName;
                                StartDeliveryPage.orderTime = orderTime;
                                StartDeliveryPage.waypointAddress = waypointAddress;
                                StartDeliveryPage.destinationAddress = destinationAddress;

                                StartDeliveryPage.StoreLatittude = store_lat;
                                StartDeliveryPage.StoreLongitude = store_long;

                                StartDeliveryPage.UserLatitude = user_lat;
                                StartDeliveryPage.userLongitude = user_long;

                                OrderTimer(false);
                                DashboardPage.time = false;// remove this if you want to remove the timer
                                DashboardPage._OrderList.Add(new Models.eCommerce.Transactions() { ID = item_id }); // Add orderid to observable collection.
                                StartDeliveryPage.order_status = "preparing";

                                PopupNavigation.Instance.PopAsync();
                                await Navigation.PushModalAsync(new StartDeliveryPage());
                                IsRunning.IsRunning = false;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                IsRunning.IsRunning = false;
                            }
                        });
                    }
                }
                catch (Exception e)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                    IsRunning.IsRunning = false;
                }
            } 
           

            
            
        }
    }
}