using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
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
			Locations.Instance.Countries("datavice", (bool success, string data) =>
            {
                if (success)
                {
                    CountryData country = JsonConvert.DeserializeObject<CountryData>(data);
                    for (int i = 0; i < country.data.Length; i++)
                    {
						string code = country.data[i].code;
						string name = country.data[i].name;
						countryCollection.Add(new CountryData() { CountryCode = code, Name = name });
					}
                }
                else
				{
					new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
					countryCollection.Clear();

				}
            });
		}
	}
}
