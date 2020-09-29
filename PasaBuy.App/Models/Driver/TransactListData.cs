using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class TransactListData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private string item_id = string.Empty;
        private string store = string.Empty;
        private string customer = string.Empty;
        private string product = string.Empty;
        private string price = string.Empty;
        private string quantity = string.Empty;
        private string status = string.Empty;
        private string date_created = string.Empty;
        private string date_ordered = string.Empty;


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



        public OrderData[] data;
        public class OrderData
        {
            public string item_id = string.Empty;
            public string store = string.Empty;
            public string customer = string.Empty;
            public string product = string.Empty;
            public string price = string.Empty;
            public string quantity = string.Empty;
            public string status = string.Empty;
            public string date_created = string.Empty;
            public string date_ordered = string.Empty;
        }

    }

}
