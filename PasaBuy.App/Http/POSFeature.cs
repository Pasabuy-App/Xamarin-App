using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http
{
    public class POSFeature
    {
        #region Fields
        /// <summary>
        /// Instance of Customer Process Ordering Class with cancel, create, delete and update method.
        /// </summary>
        private static POSFeature instance;
        public static POSFeature Instance
        {
            get
            {
                if (instance == null)
                    instance = new POSFeature();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public POSFeature()
        {
            client = new HttpClient();
        }
        #endregion

        #region Method

        /// <summary>
        /// Customer create order using stid method, addid, msg and list.
        /// </summary>
        public async void CreateOrder(string stid, string method, string addid, string msg, System.Collections.ObjectModel.ObservableCollection<Models.MobilePOS.OrderModel> List, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            dict.Add("stid", stid);
            dict.Add("addid", addid);
            dict.Add("method", method);
            dict.Add("msg", msg);

            int i = 0;
            foreach (var order in List)
            {
                dict.Add("data[items][" + i + "][pdid]", order.ID.ToString());
                dict.Add("data[items][" + i + "][qty]", order.TotalQuantity.ToString());
                int ii = 0;
                foreach (var var in order.Variants)
                {
                    dict.Add("data[items][" + i + "][variants][varid" + ii + "]", var.ID.ToString());
                    ii++;
                }
                i++;
            }

            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v1/customer/order/insert", content);
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

        /// <summary>
        /// Listing of role using wpid, snky, stid, roid and status.
        /// </summary>
        public async void Role_List(string roid, string status, Action<bool, string> callback)
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

        /// <summary>
        /// Insert of role using wpid, snky, stid, title, info and access.
        /// </summary>
        public async void Role_Insert(string title, string info, System.Collections.ObjectModel.ObservableCollection<Models.MobilePOS.AccessModel> List, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            dict.Add("stid", PSACache.Instance.UserInfo.stid);
            dict.Add("title", title);
            dict.Add("info", info);

            int i = 0;
            foreach (var access in List)
            {
                dict.Add("data[access][" + i + "][value]", access.ID.ToString());
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

        /// <summary>
        /// Listing of access for role.
        /// </summary>
        public async void Access_List(Action<bool, string> callback)
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
        #endregion
    }
}
