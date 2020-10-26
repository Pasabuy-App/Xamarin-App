using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http
{
    public class HatidFeature
    {
        #region Static
        private static HatidFeature instance;
        public static HatidFeature Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HatidFeature();
                }

                return instance;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public HatidFeature()
        {
            client = new HttpClient();
        }
        #endregion

        #region Vehicle List Method
        public async void VehicList(string vehicle_id, string mover_id, string vehicle_type, string plate, string maker, string model, string owner, string status, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
            dict.Add("snky", PSACache.Instance.UserInfo.snky);
            dict.Add("vid", vehicle_id);
            dict.Add("mvid", mover_id);
            dict.Add("types", vehicle_type);
            dict.Add("plate", plate);
            dict.Add("maker", maker);
            dict.Add("model", model);
            dict.Add("owner", owner);
            dict.Add("status", status);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/hatidpress/v2/mover/vehicle/list", content);
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
