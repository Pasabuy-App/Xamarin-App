using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Onboarding
{
    public class Barangay
    {
        public BarangayData[] data;
    }
    public class BarangayData
    {
        public string code = string.Empty;
        public string name = string.Empty;
    }
}
