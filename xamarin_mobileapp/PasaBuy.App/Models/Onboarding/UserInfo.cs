using Newtonsoft.Json;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace PasaBuy.App.Models.Onboarding
{
    public class UserInfo
    {
        public string status = string.Empty;
        public string message = string.Empty;
        public string uname = string.Empty;
        public string dname = string.Empty;
        public string email = string.Empty;
        public string avatar = string.Empty;
        public string regdate = string.Empty;

        public static bool Exist
        {
            get {
                return Preferences.ContainsKey("UserInfo");
            }
        }

        public void SaveToPreference()
        {
            string data = JsonConvert.SerializeObject(this);
            Preferences.Set("UserInfo", data);
        }
    }
}
