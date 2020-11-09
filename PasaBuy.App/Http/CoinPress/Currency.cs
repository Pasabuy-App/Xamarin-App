using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.CoinPress
{
    public class Currency
    {
        #region Fields
        /// <summary>
        /// Instance of Currency Class with create method.
        /// </summary>
        private static Currency instance;
        public static Currency Instance
        {
            get
            {
                if (instance == null)
                    instance = new Currency();
                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Currency()
        {
            client = new HttpClient();
        }
        #endregion

        #region Create Method
        public async void Create(string title, string info, string abbrev, string exchange, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                dict.Add("snky", PSACache.Instance.UserInfo.snky);
                dict.Add("title", title);
                dict.Add("info", info);
                dict.Add("abbrev", abbrev);
                dict.Add("exchange", exchange);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/coinpress/v1/user/wallet/currencies", content);
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: CPV1CNY-C1.", "OK");
            }
        }
        #endregion
    }
}
