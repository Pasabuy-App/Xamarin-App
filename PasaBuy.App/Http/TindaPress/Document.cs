using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PasaBuy.App.Http.TindaPress
{
    public class Document
    {
        #region Fields

        /// <summary>
        /// Instance of Store document.
        /// </summary>
        private static Document instance;
        public static Document Instance
        {
            get
            {
                if (instance == null)
                    instance = new Document();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Document()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method
        public async void TypeListing(string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("status", status);

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/document/type/list", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-DT1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-DT1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-DT1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-DT1-" + err.ToString());
                }
            }
        }

        public async void Insert(string preview, string stid, string comments, string types, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                multiForm.Add(new StringContent(preview), "preview");
                multiForm.Add(new StringContent(stid), "stid");
                multiForm.Add(new StringContent(comments), "comments");
                multiForm.Add(new StringContent(types), "types");

                if (preview != "")
                {
                    FileStream fs = File.OpenRead(preview);
                    multiForm.Add(new StreamContent(fs), "preview", Path.GetFileName(preview));
                }

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/document/insert", multiForm);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-I1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-I1-" + err.ToString());
                }
            }
        }

        public async void Listing(string stid, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("status", status);
                dict.Add("stid", stid);

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/document/list", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-L1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-L1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-L1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-L1-" + err.ToString());
                }
            }
        }


        public async void Delete(string docid, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("docid", docid);

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/document/delete", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-D1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-D1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-D1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-D1-" + err.ToString());
                }
            }
        }

        public async void Update(string preview, string docid, string types, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                multiForm.Add(new StringContent(preview), "preview");
                multiForm.Add(new StringContent(docid), "docid");
                multiForm.Add(new StringContent(types), "types");


                if (preview != "")
                {
                    FileStream fs = File.OpenRead(preview);
                    multiForm.Add(new StreamContent(fs), "preview", Path.GetFileName(preview));
                }

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/store/document/update", multiForm);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2CAT-U1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2CAT-U1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2CAT-U1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2CAT-U1-" + err.ToString());
                }
            }
        }
        #endregion
    }
}
