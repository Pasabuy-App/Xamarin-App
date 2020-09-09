using System;
using System.Collections.ObjectModel;
using DataVice;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.ViewModels.Onboarding
{
	public class BrgyViewModel : BaseViewModel
	{
		public static ObservableCollection<BrgyData> brgyCollection;
		public ObservableCollection<BrgyData> BrgyCollection
		{
			get { return brgyCollection; }
			set { brgyCollection = value; this.NotifyPropertyChanged(); }
		}
		public BrgyViewModel(string val)
		{
			brgyCollection = new ObservableCollection<BrgyData>();
            try
			{
				Locations.Instance.Barangays(val, "datavice", (bool success, string data) =>
				{
					if (success)
					{
						BrgyData brgy = JsonConvert.DeserializeObject<BrgyData>(data);
						for (int i = 0; i < brgy.data.Length; i++)
						{
							string id = brgy.data[i].code;
							string name = brgy.data[i].name;
							brgyCollection.Add(new BrgyData() { BrgyCode = id, Name = name });
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
		public static void ClearList()
        {
			brgyCollection.Clear();
			brgyCollection.Add(new BrgyData() { BrgyCode = "", Name = "Select City First" });
		}
	}
}
