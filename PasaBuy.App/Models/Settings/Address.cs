﻿using Xamarin.Forms.Internals;

namespace PasaBuy.App.Models.Settings
{
    /// <summary>
    /// Model for my address page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Address
    {
        #region Properties
        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string SelectedAddress { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the contact number.
        /// </summary>
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string AddressPhoto { get; set; }
        public bool isPerson { get; set; }
        public bool isPhone { get; set; }
        public bool isPhoto { get; set; }
        public bool isType { get; set; }
        public bool isLocation { get; set; }
        #endregion
    }
}
