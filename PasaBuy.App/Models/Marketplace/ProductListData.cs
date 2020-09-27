using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
    public class ProductListData
    {
        public ProductData[] data;
        public class ProductData
        {
            public string ID = string.Empty;
            public string stid = string.Empty;
            public string catid = string.Empty;
            public string total = string.Empty;
            public string store_name = string.Empty;
            public string cat_name = string.Empty;
            public string product_name = string.Empty;  
            public string short_info = string.Empty;
            public string long_info = string.Empty;
            public string sku = string.Empty;
            public double price = 0;
            public string weight = string.Empty;
            public string preview = string.Empty;
            public string dimension = string.Empty;
            public string status = string.Empty;
        }

    }
}
