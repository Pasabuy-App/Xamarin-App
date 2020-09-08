﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Settings
{
    public class AddressData
    {
        public addressData[] data;
        public class addressData
        {
            public string id = string.Empty;
            public string types = string.Empty;
            public string status = string.Empty;
            public string street = string.Empty;
            public string brgy = string.Empty;
            public string city = string.Empty;
            public string province = string.Empty;
            public string country = string.Empty;
        }
        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string province;
        public string Province
        {
            get { return province; }
            set { province = value; }
        }
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string brgy;
        public string Brgy
        {
            get { return brgy; }
            set { brgy = value; }
        }
        private string street;
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string types;
        public string Types
        {
            get { return types; }
            set { types = value; }
        }
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}