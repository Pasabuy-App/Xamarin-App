using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasaBuy.App.Controllers;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PasaBuy.App
{
    public class UserAuth
    {
        public string UN { get; set; }
        public string PW { get; set; }
        public UserAuth(string username, string password)
        {
            this.UN = username;
            this.PW = password;
        }
    }

    /// <summary>
    /// Main interface for RestAPI.
    /// </summary>
    public class RestService
    {
        #region Fields

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static RestService instance;
        public static RestService Instance
        {
            get
            {
                if (instance == null)
                    instance = new RestService();
                return instance;
            }
        }

        /// <summary>
        /// Primary base url for fetching custom data.
        /// </summary>
        string BaseApiUrl { get; } = "http://10.0.2.2/wp-json/pasabuy/v1/";

        /// <summary>
        /// Default WordPress RestAPI base url.
        /// </summary>
        string DemoApiUrl { get; } = "http://10.0.2.2/wp-json/wp/v2/"; 

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;

        #endregion

        #region Constructor
        public RestService()
        {
            client = new HttpClient();
        }
        #endregion

        #region Methods
        public async void Authenticate(string username, string password, Action<bool, string> callback)
        {
            var dict = new Dictionary<string, string>();
                dict.Add("UN", username);
                dict.Add("PW", password);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseApiUrl + "user/auth", content);
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

        public async void GetUserInfo(Action<bool, string> callback)
        {
            string utok = Preferences.Get("UserToken", "");
            Token userToken = JsonConvert.DeserializeObject<Token>(utok);

            var dict = new Dictionary<string, string>();
                dict.Add("wpid", userToken.wpid);
                dict.Add("snid", userToken.snid);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseApiUrl + "user/verify", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                UserInfo info = JsonConvert.DeserializeObject<UserInfo>(result);
                    info.SaveToPreference();

                bool success = info.status == "success" ? true : false;
                string data = info.status == "success" ? result : info.message;
                callback(success, data);
            }

            else
            {
                callback(false, "Network Error! Check your connection.");
            }
        }

        public async void WP_Post()
        {
            HttpResponseMessage response = await client.GetAsync(DemoApiUrl + "posts");

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("BytesCrafter: Data -> " + result);
                //var temp = JsonConvert.DeserializeObject(str);
                //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
            }
            
            else
            {
                Debug.WriteLine("BytesCrafter: Failed! -> " + response.StatusCode);
            }

        }
        #endregion
    }
}
