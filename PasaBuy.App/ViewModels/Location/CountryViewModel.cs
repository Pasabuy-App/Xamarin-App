using PasaBuy.App.Models.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace PasaBuy.App.ViewModels.Location
{
    public class CountryViewModel : BaseViewModel
    {
        public ObservableCollection<Country> CountryList
        {
            get;
            set;
        }
        public CountryViewModel()
        {
            CountryList = new ObservableCollection<Country>();
            //CountryList.Add(new Countr)
            Debug.WriteLine(CountryList);
            return;
        }

    }
}
