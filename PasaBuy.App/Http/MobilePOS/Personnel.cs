using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.MobilePOS
{
    public class Personnel
    {
        #region Fields

        /// <summary>
        /// Instance of store personnel.
        /// </summary>
        private static Personnel instance;
        public static Personnel Instance
        {
            get
            {
                if (instance == null)
                    instance = new Personnel();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Personnel()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing of personnel.
        /// </summary>
        public async void Listing(string user_id, string plid, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("user_id", user_id);
                    dict.Add("plid", plid);
                    dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/list", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-L1.", "OK");
            }
        }

        /// <summary>
        /// Inserting of personnel.
        /// </summary>
        public async void Insert(string user_id, string roid, string pincode, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("stid", PSACache.Instance.UserInfo.stid);
                dict.Add("user_id", user_id);
                dict.Add("roid", roid);
                dict.Add("pincode", pincode);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/insert", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-I1.", "OK");
            }
        }

        /// <summary>
        /// Update of personnel.
        /// </summary>
        public async void Update(string pid, string roid, string pincode, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("pid", pid);
                    dict.Add("roid", roid);
                    dict.Add("pincode", pincode);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/update", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-U1.", "OK");
            }
        }

        /// <summary>
        /// Deleting of personnel.
        /// </summary>
        public async void Delete(string pid, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("pid", pid);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/delete", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-D1.", "OK");
            }
        }

        /// <summary>
        /// Listing of store of personnel.
        /// </summary>
        public async void Store_List(string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/store/assigned", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2PNL-LS1.", "OK");
            }
        }

        #endregion
    }
}
