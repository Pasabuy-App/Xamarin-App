﻿
namespace PasaBuy.App.Models.Onboarding
{
    public class CountryData
    {
        public CountryList[] data;
        public class CountryList
        {
            public string code = string.Empty;
            public string name = string.Empty;
        }
        private string code;
        public string CountryCode
        {
            get { return code; }
            set { code = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
