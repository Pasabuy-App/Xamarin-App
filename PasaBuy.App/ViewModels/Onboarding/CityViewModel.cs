using System;
using System.Collections.ObjectModel;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.ViewModels.Onboarding
{
	public class CityViewModel : BaseViewModel
	{
		public static ObservableCollection<CityData> cityCollection;
		public ObservableCollection<CityData> CityCollection
		{
			get { return cityCollection; }
			set { cityCollection = value; this.NotifyPropertyChanged(); }
		}
		public CityViewModel(string val)
		{
			cityCollection = new ObservableCollection<CityData>();
            try
			{
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
						new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
					}
				});
			}
			catch (Exception)
			{
				new Alert("Something went Wrong", "Please contact administrator. Error Code: 20411.", "OK");
			}
		}
		public static void ClearList()
		{
			cityCollection.Clear();
			cityCollection.Add(new CityData() { CityCode = "", Name = "Select Province First" });
		}
	}
}
