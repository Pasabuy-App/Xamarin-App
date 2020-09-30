using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
    public class ProductList
    {
        public string stid = string.Empty;
        public string id = string.Empty;
        public string previewImage = string.Empty;
        public string name = string.Empty;
        public string summary = string.Empty;
        public string description = string.Empty;
        public double actualprice = 0;
        public double discountPrice = 0;
        public double discountPercent = 0;
        private int totalQuantity;

        public string Stid
        {
            get { return stid; }
            set
            {
                stid = value;
                OnPropertyChanged("Stid");
            }
        }
        public double DiscountPercent
        {
            get
            {
                return this.discountPercent;
            }

            set
            {
                this.discountPercent = value;
                OnPropertyChanged("DiscountPercent");
            }
        }
        public double DiscountPrice
        {
            get
            {
                return this.ActualPrice - (this.ActualPrice * (this.DiscountPercent / 100));
            }

            set
            {
                this.discountPrice = value;
                OnPropertyChanged("DiscountPrice");
            }
        }
        public string PreviewImage
        {
            get { return previewImage; }
            set
            {
                previewImage = value;
                OnPropertyChanged("PreviewImage");
            }
        }
        public double ActualPrice
        {
            get { return actualprice; }
            set
            {
                actualprice = value;
                OnPropertyChanged("ActualPrice");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                OnPropertyChanged("Summary");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public int TotalQuantity
        {
            get
            {
                return this.totalQuantity;
            }

            set
            {
                this.totalQuantity = value;
                OnPropertyChanged("TotalQuantity");
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
