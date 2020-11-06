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
        public double meters;
        public double price;
        public string distance;
        public string duration;
    }
}
