using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.TindaPress
{
    public class Store
    {
        #region Fields

        /// <summary>
        /// Instance of Store listing for Food and Drinks, Store, Grocery and Partner.
        /// </summary>
        private static Store instance;
        public static Store Instance
        {
            get
            {
                if (instance == null)
                    instance = new Store();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Store()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing of store.
        /// </summary>
        public async void Listing(string stid, string title, string type, string scid, string groups, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", stid);
                    dict.Add("title", title);
                    dict.Add("type", type);
                    dict.Add("status", status);
                    dict.Add("scid", scid);
                    dict.Add("groups", groups);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/list", content);
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
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1.", "OK");
            }
        }

        /// <summary>
        /// Listing of Featured store.
        /// </summary>
        public async void Featured(string type, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("status", status);
                    dict.Add("type", type);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/featured/list", content);
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
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-F1.", "OK");
            }
        }

        #endregion
    }
}
