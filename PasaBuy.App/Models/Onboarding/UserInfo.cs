using Newtonsoft.Json;
using PasaBuy.App.Local;
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
        public string verify = "UNVERIFIED";
        public string city = string.Empty;
        public string email = string.Empty;
        public string lname = string.Empty;
        public string fname = string.Empty;
        public string banner = string.Empty;
        public string avatar = string.Empty;
        public string date_registered = string.Empty;
        public string wpid = string.Empty;
        public string snky = string.Empty;


        public UserInfoData data = new UserInfoData();

        public class UserInfoData
        {
            public string uname = string.Empty;
            public string dname = string.Empty;
            public string city = string.Empty;
            public string email = string.Empty;
            public string lname = string.Empty;
            public string fname = string.Empty;
            public string banner = string.Empty;
            public string avatar = string.Empty;
            public string date_registered = string.Empty;
            public string wpid = string.Empty;
            public string snky = string.Empty;
            public string verify = "UNVERIFIED";
        }

        public string avatarUrl
        {
            get
            {
                //return PSAConfig.baseRestUrl + avatar;
                if (avatar != "")
                {
                    if (PSAConfig.baseRestUrl != avatar.Substring(0, PSAConfig.baseRestUrl.Length))
                    {
                        return PSAConfig.baseRestUrl + avatar.Substring(PSAConfig.baseRestUrl.Length + 1);
                    }
                }
                return avatar;
            }
        }
        public string bannerUrl
        {
            get
            {
                //return PSAConfig.baseRestUrl + banner;
                if (banner != "")
                {
                    if (PSAConfig.baseRestUrl != banner.Substring(0, PSAConfig.baseRestUrl.Length))
                    {
                        return PSAConfig.baseRestUrl + banner.Substring(PSAConfig.baseRestUrl.Length + 1);
                    }
                }
                return banner;
            }
        }
        public string regdate = string.Empty;
        public bool Succeed
        {
            get
            {
                return this.status == "success" ? true : false;
            }
        }

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
