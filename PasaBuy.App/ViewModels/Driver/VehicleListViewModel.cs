using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public VehicleListViewModel()
        {
            _vehicleList = new ObservableCollection<VehicleList>();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                HatidPress.Vehicles.Instance.list(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        VehicleList datas = JsonConvert.DeserializeObject<VehicleList>(data);
                        for (int i = 0; i < datas.data.Length; i++ )
                        {
                            _vehicleList.Add(new VehicleList()
                            {
                                Identification = datas.data[i].ID,
                                VehicleType = datas.data[i].vehicle_type.ToUpper(),
                                VehicleImage = datas.data[i].vehicle,
                                Status = datas.data[i].status.ToUpper()
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

        public ICommand MyVehicleCommand
        {
            get
            {
                return new Command<string>((x) => LoadDetails(x));
            }
        }

        public async void LoadDetails(string Identification)
        {

            if (!IsBusy)
            {
                IsBusy = true;
               
                MasterView.MyType = "mover";
                new Alert("some", Identification, "ok");
                  
                App.Current.MainPage = new DashboardPage();

                IsBusy = false;
               // CanNavigate = true;
            }
          
        }
        #endregion


    }
}
