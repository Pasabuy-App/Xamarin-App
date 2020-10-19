using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class VehicleList
    {
        private string vehicle_type = string.Empty;
        private string image = string.Empty;
        private string status = string.Empty;

        public string VehicleType
        {
            get
            {
                return vehicle_type;
            }
            set
            {
                vehicle_type = value;
                OnPropertyChanged("VehicleType");
            }
        }

        public string VehicleImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("VehicleImage");
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
