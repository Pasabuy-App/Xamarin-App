using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PasaBuy.App.Models.TindaFeature
{
    public class ProductModel : INotifyPropertyChanged
    {
        public ProductsData[] data;

        private int _isDeleteCol = 0;
        public int isDeleteCol
        {
            get
            {
                return _isDeleteCol;
            }
            set
            {
                _isDeleteCol = value;
                OnPropertyChanged("isDeleteCol");
            }
        }

        private bool _isDelete = false;
        public bool isDelete
        {
            get
            {
                return _isDelete;
            }
            set
            {
                _isDelete = value;
                OnPropertyChanged("isDelete");
            }
        }

        private bool _isUpdate = false;
        public bool isUpdate
        {
            get
            {
                return _isUpdate;
            }
            set
            {
                _isUpdate = value;
                OnPropertyChanged("isUpdate");
            }
        }

        private string discount = string.Empty;
        public string Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                OnPropertyChanged("Discount");
            }
        }

        private string category_name = string.Empty;
        public string Category_Name
        {
            get { return category_name; }
            set
            {
                category_name = value;
                OnPropertyChanged("Category_Name");
            }
        }

        private string preview = string.Empty;
        public string Preview
        {
            get { return preview; }
            set
            {
                preview = value;
                OnPropertyChanged("Preview");
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
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

        private string product_name = string.Empty;
        public string Product_name
        {
            get { return product_name; }
            set
            {
                product_name = value;
                OnPropertyChanged("Product_name");
            }
        }

        private string short_info = string.Empty;
        public string Short_info
        {
            get { return short_info; }
            set
            {
                short_info = value;
                OnPropertyChanged("Short_info");
            }
        }

        private string pcid = string.Empty;
        public string Category_ID
        {
            get { return pcid; }
            set
            {
                pcid = value;
                OnPropertyChanged("Category_ID");
            }
        }

        private string inventory = string.Empty;
        public string Inventory
        {
            get { return inventory; }
            set
            {
                inventory = value;
                OnPropertyChanged("Inventory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public class ProductsData
    {
        public string ID = string.Empty;
        public string title = string.Empty;
        public string info = string.Empty;
        public string avatar = string.Empty;
        public string price = string.Empty;
        public string category_name = string.Empty;
        public string pcid = string.Empty;
        public string inventory = string.Empty;
        public string discount = string.Empty;
    }
}
