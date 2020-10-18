using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class OrderModel
    {
        private string id { get; set; }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        private int qty { get; set; }
        public int TotalQuantity
        {
            get { return qty; }
            set { qty = value; }
        }
    }
}
