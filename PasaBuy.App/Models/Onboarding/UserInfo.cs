using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
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
        public string user_type = string.Empty;
        public string stid = string.Empty;
        public string store_name = string.Empty;
        public string roid = string.Empty;
        public string store_logo = string.Empty;
        public string store_banner = string.Empty;

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
            public string user_type = string.Empty;
            public string stid = string.Empty;
            public string store_name = string.Empty;
            public string roid = string.Empty;
            public string store_logo = string.Empty;
            public string store_banner = string.Empty;
        }

        public string avatarUrl
        {
            get
            {
                return avatar;
            }
        }
        public string bannerUrl
        {
            get
            {
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
