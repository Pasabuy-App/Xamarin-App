using System.ComponentModel;

namespace PasaBuy.App.Models.Driver
{
    public class VehicleList : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string ID = string.Empty;
        private string vehicle_type = string.Empty;
        private string vehicle = string.Empty;
        private string status = string.Empty;

        public string Identification
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
                OnPropertyChanged("VehicleType");
            }
        }

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
                return vehicle;
            }
            set
            {
                vehicle = value;
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

        public VehicleListData[] data;
        public class VehicleListData
        {
            public string ID = string.Empty;
            public string vehicle_type = string.Empty;
            public string vehicle = string.Empty;
            public string status = string.Empty;
        }

    }
}
