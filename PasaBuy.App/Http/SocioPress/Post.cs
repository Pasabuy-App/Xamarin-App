using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PasaBuy.App.Http.SocioPress
{
    public class Post
    {
        #region Fields
        /// <summary>
        /// Instance of Post.
        /// </summary>
        private static Post instance;
        public static Post Instance
        {
            get
            {
                if (instance == null)
                    instance = new Post();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Post()
        {
            client = new HttpClient();
        }
        #endregion

        #region Method
        public async void Insert(string title, string content, string type, string img,
            string item_cat, string time_price, string pic_loc, string dp_loc, string vhl_date, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                multiForm.Add(new StringContent(title), "title");
                multiForm.Add(new StringContent(content), "content");
                multiForm.Add(new StringContent(type), "type");
                if (type == "pasabay")
                {
                    multiForm.Add(new StringContent(time_price), "time_price");
                    multiForm.Add(new StringContent(vhl_date), "vhl_date");
                    multiForm.Add(new StringContent(pic_loc), "pic_loc");
                    multiForm.Add(new StringContent(dp_loc), "dp_loc");
                }
                if (type == "sell")
                {
                    multiForm.Add(new StringContent(item_cat), "item_cat");
                    multiForm.Add(new StringContent(time_price), "time_price");
                    multiForm.Add(new StringContent(vhl_date), "vhl_date");
                    multiForm.Add(new StringContent(pic_loc), "pic_loc");
                }
                if (type == "pahatid")
                {
                    multiForm.Add(new StringContent(dp_loc), "dp_loc");
                    multiForm.Add(new StringContent(time_price), "time_price");
                    multiForm.Add(new StringContent(vhl_date), "vhl_date");
                    multiForm.Add(new StringContent(pic_loc), "pic_loc");
                }
                if (type == "pabili")
                {
                    multiForm.Add(new StringContent(time_price), "time_price");
                    multiForm.Add(new StringContent(vhl_date), "vhl_date");
                    multiForm.Add(new StringContent(pic_loc), "pic_loc");
                }
                if (img != "")
                {
                    FileStream fs = File.OpenRead(img);
                    multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));
                }

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/sociopress/v1/post/insert", multiForm);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-I1.", "OK");
            }
        }

        public async void Profile(string user_id, string last_id, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("user_id", user_id);
                if (last_id != "") { dict.Add("lid", last_id); }

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/sociopress/v1/feeds/profile", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-P1.", "OK");
            }
        }

        public async void Home(string last_id, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                if (last_id != "") { dict.Add("lid", last_id); }

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/sociopress/v1/feeds/home", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1PST-H1.", "OK");
            }
        }

        #endregion
    }
}
