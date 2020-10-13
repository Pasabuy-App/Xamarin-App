using PasaBuy.App.Models.Locations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PasaBuy.App.ViewModels.Location
{
    public class CountryViewModel : BaseViewModel
    {
        internal IEnumerable<object> countryCollection;

        public ObservableCollection<Country> CountryList
        {
            get;
            set;
        }
        public IEnumerable<object> CountryCollection { get; internal set; }

        public CountryViewModel()
        {
            CountryList = new ObservableCollection<Country>();
            //CountryList.Add(new Countr)
            Debug.WriteLine(CountryList);
            return;
        }

    }
}
