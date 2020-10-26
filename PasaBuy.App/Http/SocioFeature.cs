using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.IO;
using System.Net.Http;

namespace PasaBuy.App.Http
{
    public class SocioFeature
    {
        #region Fields
        /// <summary>
        /// Instance of Social Feature.
        /// </summary>
        private static SocioFeature instance;
        public static SocioFeature Instance
        {
            get
            {
                if (instance == null)
                    instance = new SocioFeature();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public SocioFeature()
        {
            client = new HttpClient();
        }
        #endregion

        #region Create Method
        public async void Post_Insert(string title, string content, string type, string img,
            string item_cat, string time_price, string pic_loc, string dp_loc, string vhl_date, Action<bool, string> callback)
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
        #endregion
    }
}
