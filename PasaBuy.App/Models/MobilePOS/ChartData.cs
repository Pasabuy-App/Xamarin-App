using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class ChartData
    {
        private float total_sales;
        private float ave_sales;
        private int total_orders;
        private string name;
        private string yvalue;


        public string Value
        {
            get
            {
                return yvalue;
            }
            set
            {
                yvalue = value;
                OnPropertyChanged("Value");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public float TotalSales
        {
            get
            {
                return total_sales;
            }
            set
            {
                total_sales = value;
                OnPropertyChanged("TotalSales");
            }
        }

        public float AverageSales
        {
            get
            {
                return ave_sales;
            }
            set
            {
                ave_sales = value;
                OnPropertyChanged("AverageSales");
            }
        }

        public int TotalOrders
        {
            get
            {
                return total_orders;
            }
            set
            {
                total_orders = value;
                OnPropertyChanged("TotalOrders");
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
