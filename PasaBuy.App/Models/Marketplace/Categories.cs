using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Syncfusion.XForms.Buttons;
using System.Collections.ObjectModel;

namespace PasaBuy.App.Models.Marketplace
{
    public class Categories
    {
   
        #region Fields

        private string ID = string.Empty;
        private string types = string.Empty;
        private string total = string.Empty;
        private string title = string.Empty;
        private string info = string.Empty;
        private string status = string.Empty;

        public ObservableCollection<ProductList> Prods { get; set; }

        #endregion

        #region properties

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
    }
}
