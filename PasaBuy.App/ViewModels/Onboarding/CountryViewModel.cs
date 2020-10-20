using DataVice;
using Forms9Patch;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;
using Alert = PasaBuy.App.Controllers.Notice.Alert;

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
            try
            {
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
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
    }
}
