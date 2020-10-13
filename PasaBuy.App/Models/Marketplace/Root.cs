using System.Collections.Generic;

namespace PasaBuy.App.Models.Marketplace
{
    public class Root
    {
        public CategoriesData[] data;
        public class CategoriesData
        {
            public string ID { get; set; }
            public string types { get; set; }
            public string stid { get; set; }
            public string total { get; set; }
            public string title { get; set; }
            public string info { get; set; }
            public string status { get; set; }
            public List<CatProduct> products { get; set; }
        }
    }
}
