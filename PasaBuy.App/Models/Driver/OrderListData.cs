using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PasaBuy.App.Models.Driver
{
    public class OrderListData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        #region Feilds

        private string item_id = string.Empty;
        private string store = string.Empty;
        private string store_address = string.Empty;
        private double store_lat = 0;
        private double store_long = 0;
        private string customer = string.Empty;
        private double customer_lat = 0 ;
        private double customer_long = 0;
        private string customer_address = string.Empty;
        private string product = string.Empty;
        private string price = string.Empty;
        private string quantity = string.Empty;
        private string status = string.Empty;
        private string date_created = string.Empty;
        private string date_ordered = string.Empty;

        #endregion

        #region Properties

        public string ItemID
        {
            get { return item_id; }
            set
            {
                item_id = value;
                OnPropertyChanged("ItemID");
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }


        public string Store_address
        {
            get { return store_address; }
            set
            {
                store_address = value;
                OnPropertyChanged("Store_address");
            }
        }

        public double Store_lat
        {
            get { return store_lat; }
            set
            {
                store_lat = value;
                OnPropertyChanged("Store_lat");
            }
        }

        public double Store_long
        {
            get { return store_long; }
            set
            {
                store_long = value;
                OnPropertyChanged("Store_lat");
            }
        }


        public string Customer_address
        {
            get { return customer_address; }
            set
            {
                customer_address = value;
                OnPropertyChanged("Store_lat");
            }
        }

        public double Customer_lat
        {
            get { return customer_lat; }
            set
            {
                customer_lat = value;
                OnPropertyChanged("Store_lat");
            }
        }

        public double Customer_long
        {
            get { return customer_long; }
            set
            {
                customer_long = value;
                OnPropertyChanged("Store_lat");
            }
        }

        public string Customer
        {
            get { return customer; }
            set
            {
                customer = value;
                OnPropertyChanged("Customer");
            }
        }

        public string Store
        {
            get { return store; }
            set
            {
                store = value;
                OnPropertyChanged("Store");
            }
        }

        public string Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
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

        public string Date_ordered
        {
            get { return date_ordered; }
            set
            {
                date_ordered = value;
                OnPropertyChanged("Date_ordered");
            }
        }


        #endregion

        #region Method
        public DeliveriesOrderData[] data;

        public class DeliveriesOrderData
        {
            public string item_id = string.Empty;
            public string store = string.Empty;
            public string store_address = string.Empty;
            public double store_lat = 0;
            public double store_long = 0;
            public string customer = string.Empty;
            public double customer_lat = 0;
            public double customer_long = 0;
            public string customer_address = string.Empty;
            public string product = string.Empty;
            public string price = string.Empty;
            public string quantity = string.Empty;
            public string status = string.Empty;
            public string date_created = string.Empty;
            public string date_ordered = string.Empty;

        }
        #endregion

    }
}
