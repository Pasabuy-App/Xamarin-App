using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Onboarding
{
    public class Province
    {
        public ProvinceData[] data;
    }
    public class ProvinceData
    {
        public string code = string.Empty;
        public string name = string.Empty;
    }
}
