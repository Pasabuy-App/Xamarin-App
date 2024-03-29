﻿
using Syncfusion.XForms.Buttons;
using System.ComponentModel;

namespace PasaBuy.App.Models.TindaFeature
{
    public class OptionModel : INotifyPropertyChanged
    {
        public OptionsData[] data;

        public string vrid = string.Empty;
        public string status = string.Empty;
        public string name = string.Empty;
        public string info = string.Empty;
        public string price = string.Empty;
        public string ID = string.Empty;
        private string names = string.Empty;
        private string id = string.Empty;
        private double prices;
        public SfRadioGroupKey GroupKey { get; set; }

        private bool _isChecked;

        public bool IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                if (_isChecked == value)
                    return;

                _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public string Vrid
        {
            get
            {
                return this.vrid;
            }

            set
            {
                this.vrid = value;
                OnPropertyChanged("Vrid");
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                OnPropertyChanged("Status");
            }
        }

        public double Price
        {
            get
            {
                return this.prices;
            }

            set
            {
                this.prices = value;
                OnPropertyChanged("Price");
            }
        }

        public string Name
        {
            get
            {
                return this.names;
            }

            set
            {
                this.names = value;
                OnPropertyChanged("Name");
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public class OptionsData
    {
        public string ID = string.Empty;
        public string pdid = string.Empty;
        public string name = string.Empty;
        public string info = string.Empty;
        public string status = string.Empty;
        public string price = string.Empty;
    }
}
