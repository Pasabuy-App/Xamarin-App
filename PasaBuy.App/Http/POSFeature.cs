using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        #region Create Method
        public async void CreateOrder(string wpid, string snky, string stid, string method, string addid, string msg, System.Collections.ObjectModel.ObservableCollection<Models.MobilePOS.OrderModel> List, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("wpid", wpid);
                dict.Add("snky", snky);
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
        #endregion
    }
}
