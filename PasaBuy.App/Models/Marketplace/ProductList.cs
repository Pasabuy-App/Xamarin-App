using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.Marketplace
{
    public class ProductList
    {
        private ObservableCollection<Options> vars = new ObservableCollection<Options>();
        public ObservableCollection<Options> Variants
        {

            get
            {

                return this.vars;
            }

            set
            {
                this.vars = value;
            }
        }

        public double _Variantprice;
        public double Variantprice
        {
            get
            {
                double varprice = 0;
                foreach (Options var in Variants)
                {
                    varprice += var.Price;
                }
                return _Variantprice = varprice;
            }
            set
            {
                _Variantprice = value;
                OnPropertyChanged("Variantprice");
            }
        }

        public string _Variant_Name = string.Empty;
        public string Variant_Name
        {
            get
            {
                string var_name = "";
                foreach (Options var in Variants)
                {
                    var_name += var.Name + " ";
                }
                return _Variant_Name = var_name;
            }
            set
            {
                _Variant_Name = value;
                OnPropertyChanged("Variant_Name");
            }
        }

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

        public string avatar = string.Empty;
        public string Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                avatar = value;
                OnPropertyChanged("Avatar");
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

        private string remarks;
        public string Remarks
        {
            get
            {
                return this.remarks;
            }

            set
            {
                this.remarks = value;
                OnPropertyChanged("Remarks");
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
