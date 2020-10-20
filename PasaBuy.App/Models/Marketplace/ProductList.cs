using System.ComponentModel;

namespace PasaBuy.App.Models.Marketplace
{
    public class ProductList
    {
        public double totalprice;
        public double TotalPrice
        {
            get
            {
                return totalprice;
            }
            set
            {
                totalprice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
        public double vrid_price;
        public double Vrid_Price
        {
            get
            {
                return vrid_price;
            }
            set
            {
                vrid_price = value;
                OnPropertyChanged("Vrid_Price");
            }
        }
        public int vrid;
        public int Vrid
        {
            get
            {
                return vrid;
            }
            set
            {
                vrid = value;
                OnPropertyChanged("Vrid");
            }
        }
        public string stid = string.Empty;
        public string Stid
        {
            get
            {
                return stid;
            }
            set
            {
                stid = value;
                OnPropertyChanged("Stid");
            }
        }

        public string id = string.Empty;
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

        public string previewImage = string.Empty;
        public string PreviewImage
        {
            get
            {
                return previewImage;
            }
            set
            {
                previewImage = value;
                OnPropertyChanged("PreviewImage");
            }
        }

        public string name = string.Empty;
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

        public string summary = string.Empty;
        public string Summary
        {
            get
            {
                return summary;
            }
            set
            {
                summary = value;
                OnPropertyChanged("Summary");
            }
        }

        public string description = string.Empty;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public double actualprice = 0;
        public double ActualPrice
        {
            get
            {
                return actualprice;
            }
            set
            {
                actualprice = value;
                OnPropertyChanged("ActualPrice");
            }
        }

        public double discountPrice = 0;
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

        public double discountPercent = 0;
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

        private double quantity;
        public double Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private int totalQuantity;
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
