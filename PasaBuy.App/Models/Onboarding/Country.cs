using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Onboarding
{
    public class Country
    {
        public CountryData[] data;
    }
    public class CountryData
    {
        public string ID = string.Empty;
        public string code = string.Empty;
        public string name = string.Empty;
        public string tzone = string.Empty;
    }
}
