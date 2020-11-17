using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.POSFeature
{
    public class OrderDetailsModel : INotifyPropertyChanged
    {
        public string product_name { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string variants { get; set; }
        public string remarks { get; set; }

        public double variants_price { get; set; }


        private double _Price;
        public double Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged("Price");
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

        private string _remarks = string.Empty;
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                OnPropertyChanged("Remarks");
            }
        }

        private string _Quantity = string.Empty;
        public string Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
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