using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Onboarding
{
    public class City
    {
        public CityData[] data;
    }
    public class CityData
    {
        public string code = string.Empty;
        public string name = string.Empty;
    }
}
