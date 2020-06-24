using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.Views.Courier
{

    public class CourierMainPageMasterMenuItem
    {
        public CourierMainPageMasterMenuItem()
        {
            TargetType = typeof(CourierMainPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}