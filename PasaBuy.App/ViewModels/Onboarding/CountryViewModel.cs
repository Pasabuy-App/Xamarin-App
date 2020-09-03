using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Onboarding
{
	public class CountryViewModel
    {
		private ObservableCollection<CountryData> countryCollection;
		public ObservableCollection<CountryData> CountryCollection
		{
			get { return countryCollection; }
			set { countryCollection = value; }
		}
		public CountryViewModel()
		{
			countryCollection = new ObservableCollection<CountryData>();
			countryCollection.Add(new CountryData() { Code = 1, Name = "Frank" });
			countryCollection.Add(new CountryData() { Code = 2, Name = "James" });
			countryCollection.Add(new CountryData() { Code = 3, Name = "Steve" });
			countryCollection.Add(new CountryData() { Code = 4, Name = "Lucas" });
			countryCollection.Add(new CountryData() { Code = 5, Name = "Mark" });
			countryCollection.Add(new CountryData() { Code = 6, Name = "Michael" });
			countryCollection.Add(new CountryData() { Code = 7, Name = "Aldrin" });
			countryCollection.Add(new CountryData() { Code = 8, Name = "Jack" });
			countryCollection.Add(new CountryData() { Code = 9, Name = "Howard" });
		}
	}
}
