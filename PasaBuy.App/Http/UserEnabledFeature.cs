using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using PasaBuy.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PasaBuy.App.Http
{
    public class UserEnabledFeature
    {
        #region Static
        private static UserEnabledFeature instance;
        public static UserEnabledFeature Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserEnabledFeature();
                }

                return instance;
            }
        }
        #endregion

        #region Fields

        public bool isMover
        {
            get
            {
                Task<Result> result = CheckFeatureEnable(FeatureType.Mover);
                return result.Result.Status;
            }
        }

        private bool isSelling = false;
        public bool isStore
        {
            get
            {
                Task<Result> result = CheckFeatureEnable(FeatureType.Merchant);
                return result.Result.Status;
            }
        }

        #endregion

        #region Constructor
        public UserEnabledFeature()
        {
            //Do something on init.
        }
        #endregion

        #region Methods

        public async Task<Result> CheckFeatureEnable(FeatureType featureType)
        {
            //Create an instance of result.
            Result result = new Result();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    var form = new FormUrlEncodedContent(dict);

                    var response = await httpClient.PostAsync(PSAConfig.CurrentRestUrl + featureType, form);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Result>(content);
                    }

                    else
                    {
                        result.status = "http-client";
                        result.message = response.StatusCode.ToString();
                        return result;
                        //Send a error log to the server.
                    }

                }
            }

            catch (Exception err)
            {
                result.status = "try-catch";
                result.message = err.Message;
                return result;
                //Send a error log to the App Center.
            }

        }

        #endregion

    }
}
