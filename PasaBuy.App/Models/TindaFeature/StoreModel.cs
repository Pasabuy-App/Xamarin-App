using System.ComponentModel;

namespace PasaBuy.App.Models.TindaFeature
{
    public class StoreModel : INotifyPropertyChanged
    {
        public StoresData[] data;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string open_close = string.Empty;
        public string Open_Close
        {
            get
            {
                return open_close;
            }
            set
            {
                open_close = value;
                OnPropertyChanged("Open_Close");
            }
        }

        private string offer = string.Empty;
        public string Offer
        {
            get
            {
                return offer;
            }
            set
            {
                offer = value;
                OnPropertyChanged("Offer");
            }
        }

        private string info = string.Empty;
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }

        private string id = string.Empty;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string banner = string.Empty;
        public string Banner
        {
            get
            {
                return banner;
            }
            set
            {
                banner = value;
                OnPropertyChanged("Banner");
            }
        }

        private string logo = string.Empty;
        public string Logo
        {
            get
            {
                return logo;
            }
            set
            {
                logo = value;
                OnPropertyChanged("Logo");
            }
        }

        private string address = string.Empty;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        private string city = string.Empty;
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        private string rating = string.Empty;
        public string Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }

        private string operation = string.Empty;
        public string Operation
        {
            get
            {
                return operation;
            }
            set
            {
                operation = value;
                OnPropertyChanged("Operation");
            }
        }
    }

    public class StoresData
    {
        public string ID = string.Empty;
        public string hsid = string.Empty;
        public string title = string.Empty;
        public string scid = string.Empty;
        public string info = string.Empty;
        public string avatar = string.Empty;
        public string banner = string.Empty;
        public string adid = string.Empty;
        public string status = string.Empty;
        public string created_by = string.Empty;
        public string date_created = string.Empty;
        public string category_name = string.Empty;
        public string street = string.Empty;
        public string brgy = string.Empty;
        public string brgy_code = string.Empty;
        public string city = string.Empty;
        public string city_code = string.Empty;
        public string province = string.Empty;
        public string province_code = string.Empty;
        public string country = string.Empty;
        public string country_code = string.Empty;
        public string rates = string.Empty;
        public string opening = string.Empty;
        public string closing = string.Empty;
        public string operation_id = string.Empty;
    }

}
