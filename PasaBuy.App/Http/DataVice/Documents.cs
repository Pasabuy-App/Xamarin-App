using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using DataVice.Model;
using System.IO;
using PasaBuy.App.Local;

namespace PasaBuy.App.Http.DataVice
{
    public class Documents
    {
        #region Fields 

        /// <summary>
        /// Instance of Documents Class with insert method.
        /// </summary>
        private static Documents instance;
        public static Documents Instance
        {
            get
            {
                if (instance == null)
                    instance = new Documents();
                return instance;
            }
        }

        #endregion

        #region Construtor

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;
        public Documents()
        {
            client = new HttpClient();
        }

        #endregion

        #region Method 

        /// <summary>
        /// Delete address.
        /// </summary>
        public async void Insert(string type, string doctype, string img, string number_contact, string nationality, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();

                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(type), "type");
                    multiForm.Add(new StringContent(number_contact), "number_contact");
                    if (doctype != "") { multiForm.Add(new StringContent(doctype), "doctype"); }
                    if (nationality != "") { multiForm.Add(new StringContent(nationality), "nationality"); }

                    FileStream fs = File.OpenRead(img);
                    multiForm.Add(new StreamContent(fs), "img", Path.GetFileName(img));

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/documents/insert", multiForm);
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
