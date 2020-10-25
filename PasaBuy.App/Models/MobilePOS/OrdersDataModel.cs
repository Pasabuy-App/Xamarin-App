using System.ComponentModel;

namespace PasaBuy.App.Models.MobilePOS
{
    public class OrdersDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        private string stid = string.Empty;
        public string Store_ID
        {
            get { return stid; }
            set
            {
                stid = value;
                OnPropertyChanged("Store_ID");
            }
        }
        private string user_id = string.Empty;
        public string User_ID
        {
            get { return user_id; }
            set
            {
                user_id = value;
                OnPropertyChanged("User_ID");
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
        private string stages = string.Empty;
        public string Stage
        {
            get { return stages; }
            set
            {
                stages = value;
                OnPropertyChanged("Stage");
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
        private string date_time = string.Empty;
        public string Date_Time
        {
            get { return date_time; }
            set
            {
                date_time = value;
                OnPropertyChanged("Date_Time");
            }
        }
        private string totalprice = string.Empty;
        public string TotalPrice
        {
            get { return totalprice; }
            set
            {
                totalprice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
        private string customer = string.Empty;
        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }
        private string orderID = string.Empty;
        public string OrderID
        {
            get { return orderID; }
            set
            {
                orderID = value;
                OnPropertyChanged("OrderID");
            }
        }
        private string product = string.Empty;
        public string Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }
        private string quantity = string.Empty;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public OrderData[] data;
        public class OrderData
        {
            public string ID = string.Empty;
            public string avatar = string.Empty;
            public string stid = string.Empty;
            public string user_id = string.Empty;
            public string product_name = string.Empty;
            public string customer = string.Empty;
            public string odid = string.Empty;
            public string qty = string.Empty;
            public string price = string.Empty;
            public string variant_price = string.Empty;
            public string totalprice = string.Empty;
            public string stage = string.Empty;
            public string preview = string.Empty;
            public string date_created = string.Empty;
            public string method = string.Empty;
        }
    }
}
