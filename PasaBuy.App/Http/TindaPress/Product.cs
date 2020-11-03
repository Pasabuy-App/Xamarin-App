using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PasaBuy.App.Http.TindaPress
{
    public class Product
    {
        #region Fields

        /// <summary>
        /// Instance of Product insert, update, delete, list and search.
        /// </summary>
        private static Product instance;
        public static Product Instance
        {
            get
            {
                if (instance == null)
                    instance = new Product();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Product()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Listing category.
        /// </summary>
        public async void Listing(string ID, string title, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", PSACache.Instance.UserInfo.stid);
                    dict.Add("title", title);
                    dict.Add("ID", ID);
                    dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/product/list", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-L1.", "OK");
            }
        }

        /// <summary>
        /// Delete product.
        /// </summary>
        public async void Delete(string pdid, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("pdid", pdid);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/product/delete", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: TPV2PDT-D1.", "OK");
            }
        }

        /// <summary>
        /// Insert product.
        /// </summary>
        public async void Insert(string avatar, string pcid, string title, string info, string price, string discount, string inventory, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.stid), "stid");
                    multiForm.Add(new StringContent(pcid), "pcid");
                    multiForm.Add(new StringContent(title), "title");
                    multiForm.Add(new StringContent(info), "info");
                    multiForm.Add(new StringContent(price), "price");
                    multiForm.Add(new StringContent(discount), "discount");
                    multiForm.Add(new StringContent(inventory), "inventory");
                    FileStream fs = File.OpenRead(avatar);
                    multiForm.Add(new StreamContent(fs), "avatar", Path.GetFileName(avatar));

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/product/insert", multiForm);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2PDT-I1.", "OK");
            }
        }

        /// <summary>
        /// Update product.
        /// </summary>
        public async void Update(string avatar, string pdid, string pcid, string title, string info, string price, string discount, string inventory, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(pdid), "pdid");
                    multiForm.Add(new StringContent(pcid), "pcid");
                    multiForm.Add(new StringContent(title), "title");
                    multiForm.Add(new StringContent(info), "info");
                    multiForm.Add(new StringContent(price), "price");
                    multiForm.Add(new StringContent(discount), "discount");
                    multiForm.Add(new StringContent(inventory), "inventory");
                FileStream fs = File.OpenRead(avatar);
                    multiForm.Add(new StreamContent(fs), "avatar", Path.GetFileName(avatar));

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/tindapress/v2/product/update", multiForm);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error: TPV2PDT-U1.", "OK");
            }
        }

        #endregion
    }
}
