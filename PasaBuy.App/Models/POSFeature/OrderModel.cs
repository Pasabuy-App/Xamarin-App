
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.POSFeature
{
    public class OrderData
    {
        public string pubkey = string.Empty;
        public string stid = string.Empty;
        public string customer = string.Empty;
        public string avatar = string.Empty;
        public string cutomer_address = string.Empty;
        public string cutomer_lat = string.Empty;
        public string cutomer_long = string.Empty;
        public string opid = string.Empty;
        public string store_name = string.Empty;
        public string store_logo = string.Empty;
        public string store_address = string.Empty;
        public string store_lat = string.Empty;
        public string store_long = string.Empty;
        public int total_price = 0;
        public string date_created = string.Empty;
        public string stages = string.Empty;
        public ObservableCollection<OrderDetailsModel> products;
    }
    public class OrderModel : INotifyPropertyChanged
    {

        public ObservableCollection<OrderDetailsModel> productList;
        public ObservableCollection<OrderDetailsModel> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("OrderList");
            }
        }

        private string stages = string.Empty;
        public string Stages
        {
            get { return stages; }
            set
            {
                stages = value;
                OnPropertyChanged("Stages");
            }
        }

        private string avatar = string.Empty;
        public string Avatar
        {
            get { return avatar; }
            set
            {
                avatar = value;
                OnPropertyChanged("Avatar");
            }
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

        private string customerName = string.Empty;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }

        private string customerAddress = string.Empty;
        public string CustomerAddress
        {
            get { return customerAddress; }
            set
            {
                customerAddress = value;
                OnPropertyChanged("CustomerAddress");
            }
        }

        private string storeAddress = string.Empty;
        public string StoreAddress
        {
            get { return storeAddress; }
            set
            {
                storeAddress = value;
                OnPropertyChanged("StoreAddress");
            }
        }

        private string method = string.Empty;
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
                OnPropertyChanged("Method");
            }
        }

        private double totalprice = 0.00;
        public double TotalPrice
        {
            get { return totalprice; }
            set
            {
                totalprice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private string date_created = string.Empty;
        public string DateTime
        {
            get { return date_created; }
            set
            {
                date_created = value;
                OnPropertyChanged("DateTime");
            }
        }

        private string storename = string.Empty;
        public string StoreName
        {
            get { return storename; }
            set
            {
                storename = value;
                OnPropertyChanged("StoreName");
            }
        }

        private string storelogo = string.Empty;
        public string StoreLogo
        {
            get { return storelogo; }
            set
            {
                storelogo = value;
                OnPropertyChanged("StoreLogo");
            }
        }

        private string pubkey = string.Empty;
        public string Pubkey
        {
            get { return pubkey; }
            set
            {
                pubkey = value;
                OnPropertyChanged("Pubkey");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public OrderData[] data;
    }
}
