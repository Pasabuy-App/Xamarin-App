using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
   public class CategoryProductList
    {
        public class Discount
        {
            public string name { get; set; }
            public string value { get; set; }
            public string expiry { get; set; }
            public string status { get; set; }
        }

        public class Product
        {
            public string ID { get; set; }
            public string stid { get; set; }
            public string catid { get; set; }
            public string total { get; set; }
            public string store_name { get; set; }
            public string cat_name { get; set; }
            public string product_name { get; set; }
            public string short_info { get; set; }
            public string long_info { get; set; }
            public string sku { get; set; }
            public string price { get; set; }
            public string weight { get; set; }
            public string preview { get; set; }
            public string dimension { get; set; }
            public string status { get; set; }
            public Discount discount { get; set; }
        }

        public class Root
        {
            public string ID { get; set; }
            public string types { get; set; }
            public string stid { get; set; }
            public string total { get; set; }
            public string title { get; set; }
            public string info { get; set; }
            public string status { get; set; }
            public List<Product> products { get; set; }
        }
    }
}
