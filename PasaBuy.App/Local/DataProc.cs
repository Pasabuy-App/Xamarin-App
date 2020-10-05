﻿using System;
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

            return current.Replace(PSAConfig.devMediaUrl, PSAConfig.devBaseUrl);
        }
    }
}
