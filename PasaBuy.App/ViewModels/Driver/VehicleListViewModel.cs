using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Driver
{
    public class VehicleListViewModel : BaseViewModel
    {
        public static ObservableCollection<VehicleList> _vehicleList;

        public ObservableCollection<VehicleList> VehicleList
        {
            get
            {
                return _vehicleList;
            }
            set
            {
                _vehicleList = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _Name;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _Avatar;

        public string Avatar
        {
            get
            {
                return _Avatar;
            }
            set
            {
                _Avatar = value;
                this.NotifyPropertyChanged();
            }
        }
        public int _Rating;

        public int Rating
        {
            get
            {
                return _Rating;
            }
            set
            {
                _Rating = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _Status;

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                this.NotifyPropertyChanged();
            }
        }
        public string _Expiry;

        public string Expiry
        {
            get
            {
                return _Expiry;
            }
            set
            {
                _Expiry = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }
        public Command SubmitCommand { get; set; }
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

        public static string status;
        public static string expiry;
        public static int rating;
        public VehicleListViewModel()
        {
            CheckLocation();

            this.Rating = rating;
            this.Status = status;
            this.Expiry = expiry;
            this.Avatar = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
            this.Name = PSACache.Instance.UserInfo.dname;

            this.SubmitCommand = new Command(this.SubmitClicked);
            this.ItemTappedCommand = new Command<object>(ItemClicked);

            _vehicleList = new ObservableCollection<VehicleList>();
            LoadVehicle();

            RefreshCommand = new Command<string>((key) =>
            {
                LoadVehicle();
                IsRefreshing = false;
            });
                                                                                                                                                                                                                                                                                                 
        }
        public async void CheckLocation()
        {

            try
            {
                var request = new Xamarin.Essentials.GeolocationRequest(Xamarin.Essentials.GeolocationAccuracy.Medium);
                var location = await Xamarin.Essentials.Geolocation.GetLocationAsync(request);
            }
            catch (Xamarin.Essentials.FeatureNotEnabledException featureNotEnabledException)
            {
                await Application.Current.MainPage.DisplayAlert("Notice to User", "Please enable your location first.", "OK");
                Xamarin.Forms.DependencyService.Get<PasaBuy.App.Library.IGpsDependencyService>().OpenSettings();

            }

            IsGpsEnable = Xamarin.Forms.DependencyService.Get<PasaBuy.App.Library.IGpsDependencyService>().IsGpsEnable();

            if (!IsGpsEnable)
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }


        public void LoadVehicle()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    _vehicleList.Clear();
                    Http.HatidPress.Vehicle.Instance.Listing_Vehicle("", "", "", "", "", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            VehicleList vehicle = JsonConvert.DeserializeObject<VehicleList>(data);
                            System.Diagnostics.Debug.WriteLine("data: " + data);
                            for (int i = 0; i < vehicle.data.Length; i++)
                            {
                                _vehicleList.Add(new VehicleList()
                                {
                                    Identification = vehicle.data[i].ID,
                                    VehicleType = vehicle.data[i].types,
                                    VehicleImage = string.IsNullOrEmpty(vehicle.data[i].preview) ? "" : PSAProc.GetUrl(vehicle.data[i].preview),
                                    Status = vehicle.data[i].status.ToUpper()
                                });
                            }
                            IsRunning = false;
                        }
                        else
                        {
                            new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2VHE-LV1VLVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2VHE-LV1VLVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2VHE-LV1VLVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2VHE-LV1VLVM-" + err.ToString());
                }
                IsRunning = false;
            }
        }
        public static void RefreshVehicle()
        {
            try
            {
                _vehicleList.Clear();
                Http.HatidPress.Vehicle.Instance.Listing_Vehicle("", "", "", "", "", "", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        VehicleList vehicle = JsonConvert.DeserializeObject<VehicleList>(data);
                        for (int i = 0; i < vehicle.data.Length; i++)
                        {
                            _vehicleList.Add(new VehicleList()
                            {
                                Identification = vehicle.data[i].ID,
                                VehicleType = vehicle.data[i].types,
                                VehicleImage = string.IsNullOrEmpty(vehicle.data[i].preview) ? "" : PSAProc.GetUrl(vehicle.data[i].preview),
                                Status = vehicle.data[i].status.ToUpper()
                            });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: HPV2VHE-LV1VLVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2VHE-LV1VLVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2VHE-LV1VLVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2VHE-LV1VLVM-" + err.ToString());
                }
            }
        }

        #region Commands

        public Command<object> ItemTappedCommand { get; set; }

        private async void ItemClicked(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                //new Alert("Agik", "asdasda", "OK");
                //string id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as VehicleList).Identification;
                //string status = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as VehicleList).Status;
                var vehicle = selectedItem as VehicleList;
                string id = vehicle.Identification;
                string status = vehicle.Status;
                PSACache.Instance.UserInfo.vhid = id;
                if (status == "ACTIVATED")
                {
                    MasterView.MyType = "mover";
                    await System.Threading.Tasks.Task.Delay(300); 
                    App.Current.MainPage = new NavigationView();
                    IsRunning = false;
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new Views.Driver.DriverDocuments());
                    IsRunning = false;
                }
            }
        }

        #endregion Commands

        private void SubmitClicked(object obj)
        {

        }
    }
}