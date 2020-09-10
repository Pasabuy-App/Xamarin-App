using PasaBuy.App.Local;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;
using System.ComponentModel;
using PasaBuy.App.Controllers;


namespace PasaBuy.App.Models.Marketplace
{

    public class Store 
    {
        #region Field

        private string id = string.Empty;
        private string catid = string.Empty;
        private string add_id = string.Empty;
        private string comm = string.Empty;
        private string cat_name = string.Empty;
        private string title = string.Empty;         // store name
        private string short_info = string.Empty;
        private string long_info = string.Empty;
        private string avatar = string.Empty;
        private string banner = string.Empty;
        private string status = string.Empty;
        private string street = string.Empty;
        private string brgy = string.Empty;
        private string city = string.Empty;
        private string province = string.Empty;
        private string country = string.Empty;
        private string lat = string.Empty;
        private string longs = string.Empty;
        private string phone = string.Empty;
        private string email = string.Empty;
        private string offer = string.Empty;
        private string itemRating = string.Empty;


        #endregion

        #region Properties

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

     
        public string Description 
        {
            get
            {
                return short_info;
            }
            set
            {
                this.short_info = value;
            } 
        }

        public string Logo
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

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                this.city = value;
            }
        }

        public string Barangay
        {
            get
            {
                return brgy;
            }
            set
            {
                this.brgy = value;
            }
        }

        public string Province
        {
            get
            {
                return province;
            }
            set
            {
                this.province = value;
            }
        }

        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                this.street = value;
            }
        }

        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                this.country = value;
            }
        }


        public string Offer 
        {
            get
            {
                return offer;
            }
            set
            {
                this.offer = value;
            }
        }
        public string ItemRating 
        {
            get
            {
                return itemRating;
            }
            set
            {
                this.itemRating = value;
            }
        }

        #endregion

    }
}
