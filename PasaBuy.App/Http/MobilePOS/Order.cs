using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.MobilePOS
{
    public class Order
    {
        #region Fields

        /// <summary>
        /// Instance of store wallet.
        /// </summary>
        private static Order instance;
        public static Order Instance
        {
            get
            {
                if (instance == null)
                    instance = new Order();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Order()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Customer create order using stid method, addid, msg and list.
        /// </summary>
        public async void Create(string stid, string opid, string adid, string dlfee, string method, System.Collections.ObjectModel.ObservableCollection<Models.MobilePOS.OrderModel> List, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", stid);
                    dict.Add("adid", adid);
                    dict.Add("opid", opid);
                    dict.Add("dlfee", dlfee);


                    int i = 0;
                    foreach (var order in List)
                    {
                        dict.Add("data[items][" + i + "][pdid]", order.ID.ToString());
                        dict.Add("data[items][" + i + "][qty]", order.TotalQuantity.ToString());
                        dict.Add("data[items][" + i + "][remarks]", order.Remarks.ToString());


                        int ii = 1;
                        foreach (var var in order.Variants)
                        {
                            dict.Add("data[items][" + i + "][variants][varid" + ii + "]", var.ID.ToString());
                            ii++;
                        }
                        i++;
                    }

                    dict.Add("data[payments][0][method]", method);
                    dict.Add("data[payments][0][value]", "");

                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/orders/insert", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-I1.", "OK");
            }
        }

        /// <summary>
        /// Customer create order using stid method, addid, msg and list.
        /// </summary>
        public async void Listing(string stid, string odid, string stages, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("stid", stid);
                    dict.Add("odid", odid);
                    dict.Add("stages", stages);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/orders/list", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-L1.", "OK");
            }
        }

        /// <summary>
        /// Update the status of order (cancelled, accepted, preparing, shipping)
        /// </summary>
        public async void UpdateStages(string odid, string stages, string status, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                    dict.Add("snky", PSACache.Instance.UserInfo.snky);
                    dict.Add("odid", odid);
                    dict.Add("stages", stages);
                    dict.Add("status", status);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/mobilepos/v2/orders/update", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: MPV2ODR-U1.", "OK");
            }
        }

        #endregion
    }
}
