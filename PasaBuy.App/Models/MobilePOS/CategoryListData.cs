using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.MobilePOS
{
    public class CategoryListData
    {
        public CategoriesData[] data;
        public class CategoriesData
        {
            public string ID = string.Empty;
            public string title = string.Empty;
            public string info = string.Empty;
        }
    }
}
