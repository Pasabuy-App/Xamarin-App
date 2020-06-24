using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.Views.Backend
{

    public class DriverMainMasterMenuItem
    {
        public DriverMainMasterMenuItem()
        {
            TargetType = typeof(DriverMainMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}