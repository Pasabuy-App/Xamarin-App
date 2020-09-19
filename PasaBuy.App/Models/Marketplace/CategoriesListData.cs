using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Marketplace
{
    public class CategoriesListData
    {

        public CategoriesData[] data;

        public class CategoriesData
        {
            public string ID = string.Empty;
            public string types = string.Empty;
            public string total = string.Empty;
            public string title = string.Empty;
            public string info = string.Empty;
            public string status = string.Empty;
        }


    }
}
