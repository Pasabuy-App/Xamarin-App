using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public class RestService
    {
        string BaseApiUrl { get; } = "http://10.0.2.2/wp-json/pasabuy/v1/";

        string DemoApiUrl { get; } = "http://10.0.2.2/wp-json/wp/v2/"; 

        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

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
                Preferences.Set("ReturnUser", true);
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
    }
}
