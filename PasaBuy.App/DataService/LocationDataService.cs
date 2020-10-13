using PasaBuy.App.Models.Locations;
using System.Collections.ObjectModel;

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
