using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.ViewModels.Onboarding
{
	public class CityViewModel
	{
		private ObservableCollection<CityData> cityCollection;
		public ObservableCollection<CityData> CityCollection
		{
			get { return cityCollection; }
			set { cityCollection = value; }
		}
		public CityViewModel(string val)
		{
				cityCollection = new ObservableCollection<CityData>();
				Locations.Instance.Cities(val, "datavice", (bool success, string data) =>
				{
					if (success)
					{
						CityData city = JsonConvert.DeserializeObject<CityData>(data);
						for (int i = 0; i < city.data.Length; i++)
						{
							string code = city.data[i].code;
							string name = city.data[i].name;
							cityCollection.Add(new CityData() { CityCode = code, Name = name });
						}
					}
					else
					{
						new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
					}
				});
		}
	}
}
