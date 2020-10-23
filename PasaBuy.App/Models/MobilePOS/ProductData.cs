using System.ComponentModel;

namespace PasaBuy.App.Models.MobilePOS
{
    public class ProductData : INotifyPropertyChanged
    {
        private string id = string.Empty;
        private string product_name = string.Empty;
        private string short_info = string.Empty;
        private string long_info = string.Empty;
        private string sku = string.Empty;
        private float price;
        private string weight = string.Empty;
        private string preview = string.Empty;
        private string dimension = string.Empty;
        public string Dimension
        {
            get { return dimension; }
            set
            {
                dimension = value;
                OnPropertyChanged("Dimension");
            }
        }
        public string Preview
        {
            get { return preview; }
            set
            {
                preview = value;
                OnPropertyChanged("Preview");
            }
        }
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
            }
        }
        public float Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public string Sku
        {
            get { return sku; }
            set
            {
                sku = value;
                OnPropertyChanged("Sku");
            }
        }
        public string Long_info
        {
            get { return long_info; }
            set
            {
                long_info = value;
                OnPropertyChanged("Long_info");
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
        public string Product_name
        {
            get { return product_name; }
            set
            {
                product_name = value;
                OnPropertyChanged("Product_name");
            }
        }
        public string Short_info
        {
            get { return short_info; }
            set
            {
                short_info = value;
                OnPropertyChanged("Short_info");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ProductsData[] data;
        public class ProductsData
        {
            public string ID = string.Empty;
            public string product_name = string.Empty;
            public string cat_name = string.Empty;
            public string short_info = string.Empty;
            public string long_info = string.Empty;
            public string sku = string.Empty;
            public string price = string.Empty;
            public string weight = string.Empty;
            public string preview = string.Empty;
            public string dimension = string.Empty;
        }
    }
}
