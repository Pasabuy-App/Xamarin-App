using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class DocumentData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string id = string.Empty;
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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
        private string date = string.Empty;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        private string status = string.Empty;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public bool _isGreen = false;
        public bool isGreen
        {
            get
            {
                if (Status == "Pending")
                {
                    _isGreen = true;
                }
                if (Status == "Approved")
                {
                    _isGreen = false;
                }
                return _isGreen;
            }
            set
            {
                if (_isGreen != value)
                {
                    _isGreen = value;
                    OnPropertyChanged("isGreen");
                }
            }
        }
        public bool _isRed = false;
        public bool isRed
        {
            get
            {
                if (Status == "Pending")
                {
                    _isRed = false;
                }
                if (Status == "Approved")
                {
                    _isRed = true;
                }
                return _isRed;
            }
            set
            {
                if (_isRed != value)
                {
                    _isRed = value;
                    OnPropertyChanged("isRed");
                }
            }
        }
        public DocumentsData[] data;
        public class DocumentsData
        {
            public string ID = string.Empty;
            public string doctype = string.Empty;
            public string approved = string.Empty;
            public string date_created = string.Empty;
        }
    }
}
