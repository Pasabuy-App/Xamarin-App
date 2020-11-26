using System;
using Newtonsoft.Json;
using System.Net.Http;
using PasaBuy.App.Models;
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
        public async void Insert(string type_id, string instruction, string type_face, string doctype, string number_contact, string id_number, string face, string id, Action<bool, string> callback)
        {
            try
            {
                var multiForm = new MultipartFormDataContent();

                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.wpid), "wpid");
                    multiForm.Add(new StringContent(PSACache.Instance.UserInfo.snky), "snky");
                    multiForm.Add(new StringContent(type_id), "data[0][type]");
                    multiForm.Add(new StringContent(doctype), "data[0][doctype]");
                    multiForm.Add(new StringContent(number_contact), "data[0][number_contact]");
                    multiForm.Add(new StringContent(id_number), "data[0][id_number]");
                    multiForm.Add(new StringContent(instruction), "data[1][instruction]");
                    multiForm.Add(new StringContent(type_face), "data[1][type]");

                    FileStream fs = File.OpenRead(face);
                    multiForm.Add(new StreamContent(fs), "face", Path.GetFileName(face));
                    FileStream fs1 = File.OpenRead(id);
                    multiForm.Add(new StreamContent(fs1), "id", Path.GetFileName(id));

                var response = await client.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/datavice/v1/user/documents/insert", multiForm);
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
                    new Controllers.Notice.Alert("Error Code: DVV1DOC-I1", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1DOC-I1-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1DOC-I1.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1DOC-I1-" + err.ToString());
                }
            }
        }

        #endregion
    }
}
