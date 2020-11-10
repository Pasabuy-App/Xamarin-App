using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class OrderDetailsModel
    {
        public OrderData[] data;
        public class OrderData
        {
            public string pubkey = string.Empty;
            public string operation = string.Empty;
            public string customer = string.Empty;
            public string avatar = string.Empty;
            public string cutomer_address = string.Empty;
            public string cutomer_lat = string.Empty;
            public string cutomer_long = string.Empty;
            public string opid = string.Empty;
            public string store_name = string.Empty;
            public string store_logo = string.Empty;
            public string store_address = string.Empty;
            public string store_lat = string.Empty;
            public string store_long = string.Empty;
            public int total_price;
            public string date_created = string.Empty;
            public string stages = string.Empty;
            public string countdown = string.Empty;
            public string order_id = string.Empty;
        }
    }
}
