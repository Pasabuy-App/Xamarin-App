using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    public class MoverDataModel
    {
        public MoverData[] data;
    }
    public class MoverData
    {
        public string mvid = string.Empty;
        public string date_created = string.Empty;
        public string mover_doc = string.Empty;
        public int rate;
    }
}