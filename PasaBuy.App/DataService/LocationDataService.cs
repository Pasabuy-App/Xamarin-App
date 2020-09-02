using PasaBuy.App.Models.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.DataService
{
    public class LocationDataService
    {
        public ObservableCollection<Country> CountryList
        {
            get;
            set;
        }


    }
}
