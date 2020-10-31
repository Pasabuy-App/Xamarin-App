using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http
{
    public class DataFeature
    {
        #region Static
        private static DataFeature instance;
        public static DataFeature Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataFeature();
                }

                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public DataFeature()
        {
            client = new HttpClient();
        }
        #endregion

        #region VerifyUser Method
        /// <summary>
        /// Verify user.
        /// </summary>
        /// <param name="callback"></param>
        public async void VerifyUser(Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/user/verify/docs", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }

        #endregion

        #region Search User

        /// <summary>
        /// Search user.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="lid"></param>
        /// <param name="callback"></param>
        public async void Search_User(string search, string lid, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("search", search);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/user/search", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }

        #endregion
    }
}
