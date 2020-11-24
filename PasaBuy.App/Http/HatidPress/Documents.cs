using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PasaBuy.App.Http.HatidPress
{
    public class Documents
    {
        #region Fields

        /// <summary>
        /// Instance of Mover vehicle.
        /// </summary>
        private static Documents instance;
        public static Documents Instance
        {
            get
            {
                if (instance == null)
                    instance = new Documents();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Documents()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing documents of vehicle.
        /// </summary>
        public async void Listing_VDocuments(string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("vhid", PSACache.Instance.UserInfo.vhid);
                    dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/hatidpress/v2/mover/vehicle/document/list", content);
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
                    new Controllers.Notice.Alert("Error Code: HPV2DOC-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2DOC-L1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-L1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2DOC-L1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Insert of documents of vehicle.
        /// </summary>
        public async void Insert_VDocuments(string img, string types, string comments, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();

                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.vhid), "vhid");
                    multiForm.Add(new StringContent(types), "types");
                    multiForm.Add(new StringContent(comments), "comments");
                    FileStream fs = File.OpenRead(img);
                    multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/hatidpress/v2/mover/vehicle/document/insert", multiForm);
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
                    new Controllers.Notice.Alert("Error Code: HPV2DOC-I1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2DOC-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2DOC-I1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Insert of documents of vehicle.
        /// </summary>
        public async void Listing_DocType(Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/hatidpress/v2/mover/vehicle/document/type/list", content);
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
                    new Controllers.Notice.Alert("Error Code: HPV2DOC-LT1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-HPV2DOC-LT1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: HPV2DOC-LT1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-HPV2DOC-LT1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
