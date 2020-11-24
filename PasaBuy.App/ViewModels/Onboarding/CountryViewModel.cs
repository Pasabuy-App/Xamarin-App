using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class CountryViewModel
    {
        public  ObservableCollection<CountryData> countryCollection;
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
                Http.DataVice.Locations.Instance.Countries("datavice", (bool success, string data) =>
                {
                    if (success)
                    {
                        CountryData country = JsonConvert.DeserializeObject<CountryData>(data);

                        for (int i = 0; i < country.data.Length; i++)
                        {
                            Console.WriteLine("Demoguy! " + country.data[i].name);
                            string code = country.data[i].code;
                            string name = country.data[i].name;
                            countryCollection.Add(new CountryData() { CountryCode = code, Name = name });
                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-C1CVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-C1CVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-C1CVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-C1CVM-" + err.ToString());
                }
            }
        }
    }
}
