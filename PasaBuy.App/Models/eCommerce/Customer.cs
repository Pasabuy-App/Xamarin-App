using Xamarin.Forms.Internals;

namespace PasaBuy.App.Models.eCommerce
{
    /// <summary>
    /// Model for review list.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Customer
    {
        /// <summary>
        /// Gets or sets the property that holds the customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with a label, which displays the customer name.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the address type.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the customer address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the customer mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        public AddressData[] data;
        public class AddressData
        {
            public string id = string.Empty;
            public string types = string.Empty;
            public string status = string.Empty;
            public string street = string.Empty;
            public string brgy = string.Empty;
            public string city = string.Empty;
            public string province = string.Empty;
            public string country = string.Empty;
            public string preview = string.Empty;
            public string contact = string.Empty;
            public string contact_type = string.Empty;
            public string contact_person = string.Empty;
            public string full_address = string.Empty;
            public string latitude = string.Empty;
            public string longitude = string.Empty;
        }
    }
}
