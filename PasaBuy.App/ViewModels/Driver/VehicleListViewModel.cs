using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.Navigation;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
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

        public Command SubmitCommand { get; set; }

        public VehicleListViewModel()
        {
            this.Avatar = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
            this.Name = PSACache.Instance.UserInfo.dname;
            this.SubmitCommand = new Command(this.SubmitClicked);
            _vehicleList = new ObservableCollection<VehicleList>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                Http.HatidFeature.Instance.Mover_Data((bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        MoverDataModel datas = JsonConvert.DeserializeObject<MoverDataModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            DateTime date = DateTime.ParseExact(datas.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            PSACache.Instance.UserInfo.mvid = datas.data[i].mvid;
                            this.Rating = datas.data[i].rate;
                            this.Status = datas.data[i].mover_doc;
                            this.Expiry = date.ToString("MMM. dd, yyyy");

                            LoadVehicle();
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        public static void LoadVehicle()
        {
            try
            {
                Http.HatidFeature.Instance.Listing_Vehicle("", PSACache.Instance.UserInfo.mvid, "", "", "", "", "", "", (bool success, string data) =>
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
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        #region Commands

        private Command<object> itemTappedCommand;
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.ItemSelected));
            }
        }
        private async void ItemSelected(object selectedItem)
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    string id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as VehicleList).Identification;
                    string status = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as VehicleList).Status;
                    PSACache.Instance.UserInfo.vhid = id;
                    if (status == "ACTIVE")
                    {
                        MasterView.MyType = "mover";
                        App.Current.MainPage = new DashboardPage();
                        IsBusy = false;
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushModalAsync(new Views.Driver.DriverDocuments());
                        IsBusy = false;
                    }
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsBusy = false;
            }
        }

        #endregion Commands

        private void SubmitClicked(object obj)
        {

        }
    }
}