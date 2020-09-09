using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;

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
            try
			{
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
						new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
					}
				});
			}
            catch (Exception e)
			{
				new Alert("Something went Wrong", "Please contact administrator.", "OK");
			}
        }
	}
}
