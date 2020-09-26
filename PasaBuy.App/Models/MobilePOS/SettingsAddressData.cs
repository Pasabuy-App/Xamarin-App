using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class SettingsAddressData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string Id = string.Empty;
        public string ID
        {
            get { return Id; }
            set
            {
                Id = value;
                OnPropertyChanged("ID");
            }
        }
        private string street = string.Empty;
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        private string brgy = string.Empty;
        public string Brgy
        {
            get { return brgy; }
            set
            {
                brgy = value;
                OnPropertyChanged("Brgy");
            }
        }
        private string city = string.Empty;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }
        private string province = string.Empty;
        public string Province
        {
            get { return province; }
            set
            {
                province = value;
                OnPropertyChanged("Province");
            }
        }
        private string country = string.Empty;
        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                OnPropertyChanged("Country");
            }
        }
        private string type = string.Empty;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }
        public AddressData[] data;
        public class AddressData
        {
            public string ID = string.Empty;
            public string street = string.Empty;
            public string brgy = string.Empty;
            public string city = string.Empty;
            public string province = string.Empty;
            public string country = string.Empty;
            public string type = string.Empty;
        }
    }
}
