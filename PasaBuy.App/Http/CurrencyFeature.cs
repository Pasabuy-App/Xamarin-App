using Newtonsoft.Json;
using PasaBuy.App.Local;
using PasaBuy.App.Models;
using PasaBuy.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PasaBuy.App.Http
{
    public class CurrencyFeature
    {
        #region Static
        private static CurrencyFeature instance;
        public static CurrencyFeature Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CurrencyFeature();
                }

                return instance;
            }
        }

        public string savingsWalletId = string.Empty;
        public string creditsWalletId = string.Empty;
        #endregion

        #region Fields

        #endregion

        #region Constructor
        public CurrencyFeature()
        {
            //Do something on init.
            savingsWalletId = CurrencyFeature.Instance.CreateAndReturn(CurrencyTypes.Savings).Result.data;
            Console.WriteLine("Savings: " + savingsWalletId);
            creditsWalletId = CurrencyFeature.Instance.CreateAndReturn(CurrencyTypes.Credits).Result.data;
            Console.WriteLine("Credits: " + creditsWalletId);
        }

        public void Initialize()
        {

        }
        #endregion

        #region Methods

        public async Task<Result> CreateAndReturn(CurrencyTypes currencyFeature)
        {
            //Create an instance of result.
            Result result = new Result();

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var dict = new Dictionary<string, string>();
                        dict.Add("wpid", PSACache.Instance.UserInfo.wpid);
                        dict.Add("snky", PSACache.Instance.UserInfo.snky);
                        dict.Add("abbv", currencyFeature.Value);
                    var form = new FormUrlEncodedContent(dict);

                    var response = await httpClient.PostAsync(PSAConfig.CurrentRestUrl + "/wp-json/coinpress/v1/user/wallet/create", form);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Result>(content);
                    }

                    else
                    {
                        result.status = "http-client";
                        result.message = response.StatusCode.ToString();
                        return result;
                        //Send a error log to the server.
                    }

                }
            }

            catch (Exception err)
            {
                result.status = "try-catch";
                result.message = err.Message;
                return result;
                //Send a error log to the App Center.
            }

        }

        #endregion

    }
}
