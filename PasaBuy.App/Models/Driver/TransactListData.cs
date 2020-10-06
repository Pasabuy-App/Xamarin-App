﻿using System;
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

        private string store_name = string.Empty;
        private string store_address = string.Empty;
        private string store_lat = string.Empty;
        private string store_long = string.Empty;
        
        private string customer = string.Empty;
        private string customer_address = string.Empty;
        private string customer_lat = string.Empty;
        private string customer_long = string.Empty;

        private string product_name = string.Empty;
        private string price = string.Empty;
        private string quantity = string.Empty;
        private string status = string.Empty;
        private string date_created = string.Empty;
        private string date_ordered = string.Empty;


        public string Customer_address
        {
            get { return customer_address; }
            set
            {
                customer_address = value;
                OnPropertyChanged("Customer_address");
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

        public string Store_lat
        {
            get { return store_lat; }
            set
            {
                store_lat = value;
                OnPropertyChanged("Store_lat");
            }
        }

        public string Store_long
        {
            get { return store_long; }
            set
            {
                store_long = value;
                OnPropertyChanged("Store_long");
            }
        }

        public string Customer_lat
        {
            get { return customer_lat; }
            set
            {
                customer_lat = value;
                OnPropertyChanged("Customer_lat");
            }
        }

        public string Customer_long
        {
            get { return customer_long; }
            set
            {
                customer_long = value;
                OnPropertyChanged("Customer_long");
            }
        }

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
            get { return store_name; }
            set
            {
                store_name = value;
                OnPropertyChanged("Store");
            }
        }

        public string Product
        {
            get { return product_name; }
            set
            {
                product_name = value;
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

            public string store_name = string.Empty;
            public string store_address = string.Empty;
            public string store_lat = string.Empty;
            public string store_long = string.Empty;

            public string customer = string.Empty;
            public string customer_address = string.Empty;
            public string customer_lat = string.Empty;
            public string customer_long = string.Empty;

            public string product_name = string.Empty;
            public string price = string.Empty;
            public string quantity = string.Empty;
            public string status = string.Empty;
            public string date_created = string.Empty;
            public string date_ordered = string.Empty;
        }

    }

}
