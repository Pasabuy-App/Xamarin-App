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
	public class BrgyViewModel
	{
		private ObservableCollection<BrgyData> brgyCollection;
		public ObservableCollection<BrgyData> BrgyCollection
		{
			get { return brgyCollection; }
			set { brgyCollection = value; }
		}
		public BrgyViewModel(string val)
		{
				brgyCollection = new ObservableCollection<BrgyData>();
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
						new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
					}
				});
		}
	}
}
