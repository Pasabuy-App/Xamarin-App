using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PasaBuy.App.Http.DataVice
{
    public class Locations
    {
        #region Fields

        /// <summary>
        /// Instance of Location Class with barangay, city, country and province method.
        /// </summary>
        private static Locations instance;
        public static Locations Instance
        {
            get
            {
                if (instance == null)
                    instance = new Locations();
                return instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Locations()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method

        /// <summary>
        /// Barangay listing.
        /// </summary>
        public async void Barangays(string city_code, string master_key, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("city_code", city_code);
                    dict.Add("mkey", master_key);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/location/brgy/active", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-B1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-B1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-B1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-B1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Cities listing.
        /// </summary>
        public async void Cities(string province_code, string master_key, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("prov_code", province_code);
                    dict.Add("mkey", master_key);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/location/city/active", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-CT1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-CT1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-CT1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-CT1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Countries listing.
        /// </summary>
        public async void Countries(string master_key, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("mkey", master_key);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/location/country/active", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-C1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-C1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-C1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-C1-" + err.ToString());
                }
            }
        }

        /// <summary>
        /// Province listing.
        /// </summary>
        public async void Provinces(string country_code, string master_key, Action<bool, string> callback)
        {
            try
            {
                var dict = new Dictionary<string, string>();
                    dict.Add("country_code", country_code);
                    dict.Add("mkey", master_key);
                var content = new FormUrlEncodedContent(dict);

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/location/province/active", content);
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
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1LOC-P1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1LOC-P1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1LOC-P1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1LOC-P1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
