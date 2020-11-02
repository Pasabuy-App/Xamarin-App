namespace PasaBuy.App.Models.Marketplace
{
    public class FeaturedStoreModel
    {
        #region Field

        public FoodStoreData[] data;

        public class FoodStoreData
        {
            public string ID = string.Empty;
            public string hsid = string.Empty;
            public string stid = string.Empty;
            public string catid = string.Empty;
            public string add_id = string.Empty;
            public string comm = string.Empty;
            public string cat_name = string.Empty;
            public string title = string.Empty;         // store name
            public string short_info = string.Empty;
            public string long_info = string.Empty;
            public string avatar = string.Empty;
            public string banner = string.Empty;
            public string status = string.Empty;
            public string street = string.Empty;
            public string brgy = string.Empty;
            public string city = string.Empty;
            public string province = string.Empty;
            public string country = string.Empty;
            public string lat = string.Empty;
            public string longs = string.Empty;
            public string phone = string.Empty;
            public string email = string.Empty;
            public string offer = string.Empty;
            public string itemRating = string.Empty;


        }

        private string ID = string.Empty;
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

        public string Banner
        {
            get
            {
                return banner;
            }
            set
            {
                this.banner = value;
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
