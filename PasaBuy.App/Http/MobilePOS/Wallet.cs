using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.MobilePOS
{
    public class Wallet
    {
        #region Fields

        /// <summary>
        /// Instance of store wallet.
        /// </summary>
        private static Wallet instance;
        public static Wallet Instance
        {
            get
            {
                if (instance == null)
                    instance = new Wallet();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Wallet()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Store wallet info and transactions list.
        /// </summary>
        public async void Info(Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("stid", PSACache.Instance.UserInfo.stid);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/wallets/info", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2WLT-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2WLT-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2WLT-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2WLT-I1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Listing of store wallet info and transactions list.
        /// </summary>
        public async void Change(string key, string user_id, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("key", key);
                    dict.Add("user_id", user_id);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/wallets/change", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: MPV2WLT-C1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2WLT-C1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2WLT-C1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2WLT-C1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
