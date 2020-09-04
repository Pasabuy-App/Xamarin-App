using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class ProvinceViewModel
	{
		private ObservableCollection<ProvincesData> provinceCollection;
		public ObservableCollection<ProvincesData> ProvinceCollection
		{
			get { return provinceCollection; }
			set { provinceCollection = value; }
		}
		public ProvinceViewModel(string val)
		{
				provinceCollection = new ObservableCollection<ProvincesData>();
				Locations.Instance.Provinces(val, "datavice", (bool success, string data) =>
				{
					if (success)
					{
						ProvincesData province = JsonConvert.DeserializeObject<ProvincesData>(data);
						for (int i = 0; i < province.data.Length; i++)
						{
							string code = province.data[i].code;
							string name = province.data[i].name;
							provinceCollection.Add(new ProvincesData() { ProvinceCode = code, Name = name });
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
