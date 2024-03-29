﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;

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
                Http.DataVice.Locations.Instance.Provinces(val, "datavice", (bool success, string data) =>
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
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-P1PVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-P1PVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-P1PVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-P1PVM-" + err.ToString());
                }
            }
        }
    }
}
