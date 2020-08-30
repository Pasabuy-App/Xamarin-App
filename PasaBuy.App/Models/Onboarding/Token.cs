using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.Models.Onboarding
{

    public class Token
    {
        public string status = string.Empty;
        public TokenData data = new TokenData();
        public string message = string.Empty;

        public static string wpids;
        public static string snkys;

        public static string wp(string x)
        {
            wpids = x;
            return "suucces";
        }
        public static string sn(string x)
        {
            snkys = x;
            return "suucces";
        }
    }

    public class TokenData
    {
        public string wpid = string.Empty;
        public string snky = string.Empty;
    }



}
