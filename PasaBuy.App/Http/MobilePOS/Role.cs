﻿using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.MobilePOS
{
    public class Role
    {
        #region Fields

        /// <summary>
        /// Instance of store role and access.
        /// </summary>
        private static Role instance;
        public static Role Instance
        {
            get
            {
                if (instance == null)
                    instance = new Role();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Role()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing of role using wpid, snky, stid, roid and status.
        /// </summary>
        public async void Listing(string roid, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("roid", roid);
                    dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/role/list", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2ROL-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ROL-L1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ROL-L1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Insert of role using wpid, snky, stid, title, info and access.
        /// </summary>
        public async void Insert(string title, string info, System.Collections.ObjectModel.ObservableCollection<Models.POSFeature.AccessModel> List, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("title", title);
                    dict.Add("info", info);

                    int i = 0;
                    string status;
                    foreach (var access in List)
                    {
                        status = access.Status == true ? "active" : "inactive";
                        dict.Add("data[access][" + i + "][value]", access.ID.ToString());
                        dict.Add("data[access][" + i + "][status]", status);
                        i++;
                    }

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/role/insert", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2ROL-I1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ROL-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ROL-I1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Update of role using wpid, snky, stid, title, info and access.
        /// </summary>
        public async void Update(string roid, string title, string info, System.Collections.ObjectModel.ObservableCollection<Models.POSFeature.AccessModel> List, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("role_id", roid);
                    dict.Add("title", title);
                    dict.Add("info", info);

                    int i = 0;
                    string status;
                    foreach (var access in List)
                    {
                        status = access.Status == true ? "active" : "inactive";
                        dict.Add("data[access][" + i + "][value]", access.ID.ToString());
                        dict.Add("data[access][" + i + "][status]", status);
                        i++;
                    }

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/role/update", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2ROL-U1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ROL-U1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-U1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ROL-U1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Delete role using roid.
        /// </summary>
        public async void Delete(string roid, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("roid", roid);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/role/delete", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2ROL-D1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ROL-D1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-D1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ROL-D1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Listing of access for role.
        /// </summary>
        public async void AccessList(Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/personnels/role/access/list", content);
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
                    new Controllers.Notice.Alert("Error Code: MPV2ROL-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-MPV2ROL-L1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ROL-L1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-MPV2ROL-L1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
