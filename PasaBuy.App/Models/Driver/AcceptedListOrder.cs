using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class AcceptedListOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Feilds

        private string id = string.Empty;
        private string mover_id = string.Empty;
        private string order_id = string.Empty;
        private string vehicle_type = string.Empty;
        private string store_name = string.Empty;

        private double waypoint_lat = 0;
        private double waypoint_long = 0;
        private double destination_lat = 0;
        private double destination_long = 0;
        private double package_distance = 0; 
        private double delivery_distance = 0;

        private string destination_address = string.Empty;
        private string waypoint_address = string.Empty;
        
        private int fee = 0;
        private string origin = string.Empty;
        private string date_accepted = string.Empty;
        private string date_created = string.Empty;

        #endregion

        #region Properties

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Mover_Id
        {
            get { return mover_id; }
            set
            {
                mover_id = value;
                OnPropertyChanged("Mover_Id");
            }
        }

        public int Fee
        {
            get { return fee; }
            set
            {
                fee = value;
                OnPropertyChanged("Fee");
            }
        }

        public string WaypointAddress
        {
            get { return waypoint_address; }
            set
            {
                waypoint_address = value;
                OnPropertyChanged("WaypointAddress");
            }
        }

        public double WaypointLat
        {
            get { return waypoint_lat; }
            set
            {
                waypoint_lat = value;
                OnPropertyChanged("WaypointLat");
            }
        }

        public double WaypointLong
        {
            get { return waypoint_long; }
            set
            {
                waypoint_long = value;
                OnPropertyChanged("WaypointLong");
            }
        }

        public string DestinationAddress
        {
            get { return destination_address; }
            set
            {
                destination_address = value;
                OnPropertyChanged("DestinationAddress");
            }
        }

        public double DestinationLat
        {
            get { return destination_lat; }
            set
            {
                destination_lat = value;
                OnPropertyChanged("DestinationLat");
            }
        }

        public double DestinationLong
        {
            get     { return destination_long; }
            set
            {
                destination_long = value;
                OnPropertyChanged("DestinationLong");
            }
        }

        public double PackageDistance
        {
            get { return package_distance; }
            set
            {
                package_distance = value;
                OnPropertyChanged("PackageDistance");
            }
        }

        public double DeliveryDistance
        {
            get { return delivery_distance; }
            set
            {
                delivery_distance = value;
                OnPropertyChanged("DeliveryDistance");
            }
        }

        public string Date_created
        {
            get { return date_created; }
            set
            {
                date_created = value;
                OnPropertyChanged("Date_created");
            }
        }

        public string DateAccepted
        {
            get { return date_accepted; }
            set
            {
                date_accepted = value;
                OnPropertyChanged("DateAccepted");
            }
        }

        public string StoreName
        {
            get { return store_name; }
            set
            {
                store_name = value;
                OnPropertyChanged("StoreName");
            }
        }

        #endregion


        #region Method
        public AcceptedOrderData[] data;

        public class AcceptedOrderData
        {
            public string id = string.Empty;
            public string mover_id = string.Empty;
            public string order_id = string.Empty;
            public string vehicle_type = string.Empty;
            public string store_name = string.Empty;
            
            public double waypoint_lat = 0;
            public double waypoint_long = 0;
            public double destination_lat = 0;
            public double destination_long = 0;
            public double package_distance = 0;
            public double delivery_distance = 0;

            public string destination_address = string.Empty;
            public string waypoint_address = string.Empty;
            public int fee = 0;
            public string origin = string.Empty;
            public string date_accepted = string.Empty;
            public string date_created = string.Empty;

        }
        #endregion

    }
}
