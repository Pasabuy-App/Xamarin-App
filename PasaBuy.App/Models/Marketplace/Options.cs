using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
    public class Options
    {
        private string name = string.Empty;
        private string id = string.Empty;
        private string price = string.Empty;
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

        public OptionsData[] data;
        public class OptionsData
        {
            public string ID = string.Empty;
            public string pdid = string.Empty;
            public string name = string.Empty;
            public string info = string.Empty;
            public string status = string.Empty;
            public string price = string.Empty;
        }

        public string Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
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
}
