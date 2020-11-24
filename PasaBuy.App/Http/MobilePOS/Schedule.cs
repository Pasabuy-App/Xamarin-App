using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.MobilePOS
{
    class Schedule
    {
        #region Fields

        /// <summary>
        /// Instance of store schedule.
        /// </summary>
        private static Schedule instance;
        public static Schedule Instance
        {
            get
            {
                if (instance == null)
                    instance = new Schedule();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Schedule()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing of store schedule.
        /// </summary>
        public async void Listing(Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/schedule/list", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2SCH-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2SCH-L1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2SCH-L1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2SCH-L1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Insert of store schedule.
        /// </summary>
        public async void Insert(string type, string started, string ended, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("type", type);
                    dict.Add("started", started);
                    dict.Add("ended", ended);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/schedule/insert", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2SCH-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2SCH-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2SCH-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2SCH-I1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
