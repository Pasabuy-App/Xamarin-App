using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PasaBuy.App.Models.Marketplace
{
    public class Categories
    {

        #region Fields

        private string ID = string.Empty;
        private string types = string.Empty;
        private string total = string.Empty;
        private string avatar = string.Empty;
        private string title = string.Empty;
        private string info = string.Empty;
        private string status = string.Empty;
        private string store_name = string.Empty;
        private string store_address = string.Empty;

        private ObservableCollection<ProductList> prods = new ObservableCollection<ProductList>();

        #endregion

        #region properties


        public string StoreName
        {
            get
            {
                return store_name;
            }
            set
            {
                store_name = value;
                OnPropertyChanged("StoreName");
            }
        }

        public string StoreAddress
        {
            get
            {
                return store_address;
            }
            set
            {
                store_address = value;
                OnPropertyChanged("StoreAddress");
            }
        }


        public ObservableCollection<ProductList> Prods
        {

            get
            {

                return this.prods;
            }

            set
            {
                this.prods = value;
            }
        }



        public string Id
        {
            get
            {
                return ID;
            }
            set
            {
                this.ID = value;
            }
        }


        public string Type
        {
            get
            {
                return types;
            }
            set
            {
                this.types = value;
            }
        }


        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                this.total = value;
            }
        }

        public string Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                this.avatar = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                this.title = value;
            }
        }

        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                this.info = value;
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                this.status = value;
            }
        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
