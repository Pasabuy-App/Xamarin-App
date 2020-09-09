using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Local
{
    public class PSAProc
    {
        public static string GetUrl(string current)
        {
            if(PSAConfig.onProduction)
            {
                return current;
            }

            if(current.Contains("https://"))
            {
                return current.Replace("https://" + PSAConfig.devBaseUrl, PSAConfig.baseRestUrl);
            }

            return current.Replace("http://" + PSAConfig.devBaseUrl, PSAConfig.baseRestUrl);
        }
    }
}
