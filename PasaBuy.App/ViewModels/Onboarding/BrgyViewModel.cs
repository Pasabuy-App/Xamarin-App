﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;

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
                Http.DataVice.Locations.Instance.Barangays(val, "datavice", (bool success, string data) =>
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
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-B1BVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-B1BVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-B1BVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-B1BVM-" + err.ToString());
                }
            }
        }
        public static void ClearList()
        {
            brgyCollection.Clear();
            brgyCollection.Add(new BrgyData() { BrgyCode = "", Name = "Select City First" });
        }
    }
}
