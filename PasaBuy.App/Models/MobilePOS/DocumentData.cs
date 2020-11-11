using System;
using System.ComponentModel;

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

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
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
        private DateTime date;
        public DateTime Date
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

        private string hsid = string.Empty;
        public string HashId
        {
            get { return hsid; }
            set
            {
                hsid = value;
                OnPropertyChanged("HashId");
            }
        }

        private string preview = string.Empty;
        public string Preview
        {
            get { return preview; }
            set
            {
                preview = value;
                OnPropertyChanged("Preview");
            }
        }

        private string typeid = string.Empty;
        public string TypeID
        {
            get { return typeid; }
            set
            {
                typeid = value;
                OnPropertyChanged("TypeID");
            }
        }
        public DocumentsData[] data;
        public class DocumentsData
        {
            public string ID = string.Empty;
            public string preview = string.Empty;
            public string hsid = string.Empty;
            public string title = string.Empty;
            public string doctype = string.Empty;
            public string approved = string.Empty;
            public string status = string.Empty;
            public string type_id = string.Empty;
            public DateTime date_created;
        }
    }
}
