using PasaBuy.App.Models.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

            for(int i = 0; i < 7; i++)
            {
                _vehicleList.Add(new VehicleList()
                {
                    VehicleType = "Bicycle",
                    VehicleImage = "https://image.flaticon.com/icons/png/512/71/71422.png",
                    Status = "Verified"
                });
            }
            
        }
    }
}
