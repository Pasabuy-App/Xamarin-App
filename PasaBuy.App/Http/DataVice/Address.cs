using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using PasaBuy.App.Models;
using System.IO;
using PasaBuy.App.Local;

namespace PasaBuy.App.Http.DataVice
{
    public class Address
    {
        #region Fields

        /// <summary>
        /// Instance of Address Class with insert, delete and listing method.
        /// </summary>
        private static Address instance;
        public static Address Instance
        {
            get
            {
                if (instance == null)
                    instance = new Address();
                return instance;
            }
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Address()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Delete address.
        /// </summary>
        public async void Delete(string id, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("id", id);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/address/delete", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /// <summary>
        /// Insert address.
        /// </summary>
        public async void Insert(string img, string type, string co, string pv, string ct, string bg, string st, string cnt,
            string cnt_type, string cnt_person, string lat, string lon, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();

                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(type), "type");
                    multiForm.Add(new StringContent(co), "co");
                    multiForm.Add(new StringContent(pv), "pv");
                    multiForm.Add(new StringContent(ct), "ct");
                    multiForm.Add(new StringContent(bg), "bg");
                    multiForm.Add(new StringContent(st), "st");
                    multiForm.Add(new StringContent(cnt), "cnt");
                    multiForm.Add(new StringContent(cnt_type), "cnt_type");
                    multiForm.Add(new StringContent(lat), "lat");
                    multiForm.Add(new StringContent(lon), "long");

                    if (cnt_person != "") { multiForm.Add(new StringContent(cnt_person), "cnt_person"); }
                    if (img != "")
                    {
                        FileStream fs = File.OpenRead(img);
                        multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));
                    }

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/address/insert", multiForm);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /// <summary>
        /// Delete address.
        /// </summary>
        public async void Listing(Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/address/list/all", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /// <summary>
        /// Update address.
        /// </summary>
        public async void Update(string id, string co, string pv, string ct, string bg, string st, string lat, string lon, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("id", id);
                    dict.Add("co", co);
                    dict.Add("pv", pv);
                    dict.Add("ct", ct);
                    dict.Add("bg", bg);
                    dict.Add("st", st);
                    dict.Add("lat", lat);
                    dict.Add("long", lon);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/address/update", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /// <summary>
        /// Select address by id address.
        /// </summary>
        public async void SelectByID(string address_id, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("address_id", address_id);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/address/select", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        #endregion
    }
}
