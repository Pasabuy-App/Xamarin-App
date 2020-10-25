using Newtonsoft.Json;
using PasaBuy.App.Local;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PasaBuy.App.Http
{
    public class TindaFeature
    {
        #region Fields
        /// <summary>
        /// Instance of TindaFeature.
        /// </summary>
        private static TindaFeature instance;
        public static TindaFeature Instance
        {
            get
            {
                if (instance == null)
                    instance = new TindaFeature();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public TindaFeature()
        {
            client = new HttpClient();
        }
        #endregion

        #region Method
        /// <summary>
        /// Method for Variants listing with options using product id.
        /// </summary>
        public async void VariantList_Options(string pdid,  Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            dict.Add("pdid", pdid);

            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v1/variants/option/list", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Models.Token token = JsonConvert.DeserializeObject<Models.Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                callback(success, data);
            }
            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }

        /// <summary>
        /// Method for Store search.
        /// </summary>
        public async void Store_Search(string search, string types, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            dict.Add("search", search);
            dict.Add("types", types);

            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v1/stores/search", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Models.Token token = JsonConvert.DeserializeObject<Models.Token>(result);

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
