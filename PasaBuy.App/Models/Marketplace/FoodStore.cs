using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace PasaBuy.App.Models.Marketplace
{

    public class FoodStore
    {

        #region Field

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

        #region Constructor
        public FoodStore()
        {
            this.FavouriteCommand = new Command(this.FavouriteButtonClicked);
        }
        #endregion



        #region Commands
        public Command FavouriteCommand { get; set; }
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


        #region Methods

        /// <summary>
        /// Invoked when the favourite button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void FavouriteButtonClicked(object obj)
        {
            var button = obj as SfButton;

            if (button == null)
            {
                return;
            }

            if (button.Text == "\ue701")
            {
                button.Text = "\ue732";
                Application.Current.Resources.TryGetValue("PrimaryColor", out var retVal);
                button.TextColor = (Color)retVal;
            }
            else
            {
                button.Text = "\ue701";
                Application.Current.Resources.TryGetValue("Gray-600", out var retVal);
                button.TextColor = (Color)retVal;
            }
        }
        #endregion



    }
}
