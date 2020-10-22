using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.Models.eCommerce
{
    /// <summary>
    /// Model for transaction history template.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Transactions : INotifyPropertyChanged
    {
        #region Fields

        private string id;
        private string customerName;

        private string transactionDescription;

        private string image;

        private string date;

        private string transactionAmount;
        private string status;

        private bool isCredited;

        #endregion

        #region EventHandler

        /// <summary>
        /// EventHandler of property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.OnPropertyChanged(nameof(Status));
            }
        }
        public string ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.OnPropertyChanged(nameof(ID));
            }
        }

        /// <summary>
        /// Gets or sets the name of an customer.
        /// </summary>
        public string CustomerName
        {
            get
            {
                return this.customerName;
            }

            set
            {
                this.customerName = value;
                this.OnPropertyChanged(nameof(CustomerName));
            }
        }

        /// <summary>
        /// Gets or sets the transaction description.
        /// </summary>
        public string TransactionDescription
        {
            get
            {
                return this.transactionDescription;
            }

            set
            {
                this.transactionDescription = value;
                this.OnPropertyChanged(nameof(TransactionDescription));
            }
        }

        /// <summary>
        /// Gets or sets the image of an user.
        /// </summary>
        public string Image
        {
            get
            {
                return this.image;
            }

            set
            {
                this.image = value;
                this.OnPropertyChanged(nameof(Image));
            }
        }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public string TransactionAmount
        {
            get
            {
                return this.transactionAmount;
            }

            set
            {
                this.transactionAmount = value;
                this.OnPropertyChanged(nameof(TransactionAmount));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the transaction type is credit.
        /// </summary>
        public bool IsCredited
        {
            get
            {
                return this.isCredited;
            }

            set
            {
                this.isCredited = value;
                this.OnPropertyChanged(nameof(IsCredited));
            }
        }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        public string Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
                this.OnPropertyChanged(nameof(Date));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">The PropertyName</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public OrderData[] data;
        public class OrderData
        {
            public string ID = string.Empty;
            public string product_name = string.Empty;
            public string store_name = string.Empty;
            public string odid = string.Empty;
            public string qty = string.Empty;
            public string price = string.Empty;
            public string totalprice = string.Empty;
            public string stage = string.Empty;
            public string store_logo = string.Empty;
            public string date_created = string.Empty;
            public string method = string.Empty;
        }
    }
}
