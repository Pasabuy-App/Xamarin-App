﻿using PasaBuy.App.Local;
using PasaBuy.App.Views.Driver;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
        public static int countdown;
        public static string user_id;
        public static string user_name;
        public static string user_avatar;
        public static bool isTimer = true;
        Stopwatch stopwatch = new Stopwatch();

        public PopupAcceptOrder()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
            Store.Text = storeName;
            Order.Text = orderName;// + " | " + orderTime;
            WaypointAddress.Text = waypointAddress;
            OriginAddress.Text = destinationAddress;
            OrderTime.Text = "30";// countdown.ToString();
            isTimer = true;
            OrderTimer(true);
            //DashboardPage.time = false;// remove this if you want to remove the timer
        }

        public void OrderTimer(Boolean flag)
        {
            int TimeLimit = 30;// countdown;
            if (flag == true)
            {
                stopwatch.Start();
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (isTimer)
                    {
                        int countdown = TimeLimit - stopwatch.Elapsed.Seconds;
                        OrderTime.Text = countdown.ToString();
                        if (countdown == 1)
                        {
                            PopupNavigation.Instance.PopAsync();
                            return false;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.Instance.PopAsync();
            OrderTimer(false);
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
            /*try
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

            if (!IsGpsEnable)
            {
                return;
            }*/

            try
            {
                if (IsRunning.IsRunning == false)
                {
                    IsRunning.IsRunning = true;
                    Http.HatidPress.Order.Instance.Accept_Order(odid, async (bool success, string data) =>
                    {
                        if (success)
                        {

                            StartDeliveryPage.item_id = item_id;
                            StartDeliveryPage.store_name = storeName;
                            StartDeliveryPage.customer_name = user_name;
                            StartDeliveryPage.customer_id = user_id;
                            StartDeliveryPage.customer_avatar = user_avatar;
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
                                                       //DashboardPage._OrderList.Add(new Models.eCommerce.Transactions() { ID = item_id }); // Add orderid to observable collection.
                            StartDeliveryPage.order_status = "Preparing";
                            DashboardPage._OrderList.Add(new Models.eCommerce.Transactions()
                            {
                                ID = item_id
                            }); ;
                            PopupNavigation.Instance.PopAsync();
                            isTimer = false;
                            await Navigation.PushModalAsync(new StartDeliveryPage());
                            IsRunning.IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning.IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2ODR-A1PUAC", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2ODR-A1PUAC-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2ODR-A1PUAC.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2ODR-A1PUAC-" + err.ToString());
                }
                IsRunning.IsRunning = false;
            }
        }
    }
}