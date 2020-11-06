using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.HatidFeature
{
    public class Delivery
    {
        //public int data;
        public DelvieryData[] data;
    }
    public class DelvieryData
    {
        public int price;
        public string distance;
        public string duration;
    }
}
