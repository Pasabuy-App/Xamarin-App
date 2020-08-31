using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
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
        public string un { get; set; }
        public string pw { get; set; }
        public UserAuth(string username, string password)
        {
            this.un = username;
            this.pw = password;
        }
    }

    /// <summary>
    /// Main interface for RestAPI.
    /// </summary>
    public class RestAPI
    {
        #region Fields

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static RestAPI instance;
        public static RestAPI Instance
        {
            get
            {
                if (instance == null)
                    instance = new RestAPI();
                return instance;
            }
        }

        /// <summary>
        /// Primary base url for fetching custom data.
        /// </summary>
        string BaseApiUrl 
        {
            get 
            {
                return App.BaseRootUrl + "/wp-json/datavice/v1/";
            }
        }

        /// <summary>
        /// Default WordPress RestAPI base url.
        /// </summary>
        string DemoApiUrl
        {
            get 
            {
                return App.BaseRootUrl + "/wp-json/datavice/v1/";
            }
        }

        /// <summary>
        /// Web service for communication to our Backend.
        /// </summary>
        HttpClient client;

        #endregion

        #region Constructor
        public RestAPI()
        {
            client = new HttpClient();
        }
        #endregion

        #region Methods
        public async void Authenticate(string username, string password, Action<bool, string> callback)
        {
            if (!App.HasInternet)
            {
                new Alert("Notice to User", "Connectivity Issue Occur! Please check your internet connection.", "Try Again");
                return;
            }

            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
                dict.Add("pw", password);
            var content = new FormUrlEncodedContent(dict);
          
            var response = await client.PostAsync(BaseApiUrl + "user/auth", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Token token = JsonConvert.DeserializeObject<Token>(result);

                bool success = token.status == "success" ? true : false;
                string data = token.status == "success" ? result : token.message;
                Token info = JsonConvert.DeserializeObject<Token>(data);
                Token.wp(info.data.wpid);
                Token.sn(info.data.snky);
                callback(success, data);

            }

            else
            {
                callback(false, "Network Error! Check your connection.");
            }            
        }


        public async void SignUp(string username, string email, string firstname, string lastname, string gender, string bday,
                string country, string province, string city, string brgy, string street, Action<bool, string> callback)
        {
            if (!App.HasInternet)
            {
                new Alert("Notice to User", "Connectivity Issue Occur! Please check your internet connection.", "Try Again");
                return;
            }

            var dict = new Dictionary<string, string>();
                dict.Add("un", username);
                dict.Add("em", email);
                dict.Add("fn", firstname);
                dict.Add("ln", lastname);
                dict.Add("gd", gender);
                dict.Add("bd", bday);
                dict.Add("co", country);
                dict.Add("pv", province);
                dict.Add("ct", city);
                dict.Add("bg", brgy);
                dict.Add("st", street);
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseApiUrl + "user/signup", content);
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

        public async void GetUserInfo(Token userToken, Action<bool, string> callback)
        {
            if (!App.HasInternet)
            {
                new Alert("Notice to User", "Connectivity Issue Occur! Please check your internet connection.", "Try Again");
                return;
            }

            var dict = new Dictionary<string, string>();
                dict.Add("wpid", Token.wpids);
                dict.Add("snky", Token.snkys);
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

        public async void Countries(Action<bool, string> callback)
        {
            if (!App.HasInternet)
            {
                new Alert("Notice to User", "Connectivity Issue Occur! Please check your internet connection.", "Try Again");
                return;
            }

            var dict = new Dictionary<string, string>();
            var content = new FormUrlEncodedContent(dict);

            var response = await client.PostAsync(BaseApiUrl + "location/country/active", content);
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
        #endregion
    }
}
