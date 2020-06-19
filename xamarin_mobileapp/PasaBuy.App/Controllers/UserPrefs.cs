using Newtonsoft.Json;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;

namespace PasaBuy.App.Controllers
{
    /// <summary>
    /// Global access of user preferences.
    /// </summary>
    public class UserPrefs
    {
        #region Fields
        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static UserPrefs instance;
        public static UserPrefs Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserPrefs();
                return instance;
            }
        }

        /// <summary>
        /// Store user token here.
        /// </summary>
        private Token token;

        /// <summary>
        /// Responsible for checking and getting user token.
        /// </summary>
        /// <returns></returns>
        public Token Token
        {
            set
            {
                token = value;
                string data = JsonConvert.SerializeObject(token);
                Preferences.Set("UserToken", data);
            }

            get
            {
                if (token != null)
                {
                    return token;
                }

                if (Preferences.ContainsKey("UserToken"))
                {
                    string data = Preferences.Get("UserToken", "{}");
                    token = JsonConvert.DeserializeObject<Token>(data);

                    if (token.status != string.Empty)
                    {
                        return token;
                    }
                }

                return token;
            }
        }

        /// <summary>
        /// Store Primary user information on memory.
        /// by Preferences
        /// </summary>
        private UserInfo userInfo;

        /// <summary>
        /// Responsible for checking and getting user prefs.
        /// </summary>
        public UserInfo UserInfo
        {
            set
            {
                userInfo = value;
                string data = JsonConvert.SerializeObject(userInfo);
                Preferences.Set("UserInfo", data);
            }

            get
            {
                if (userInfo != null)
                {
                    return userInfo;
                }

                if (Preferences.ContainsKey("UserInfo"))
                {
                    string data = Preferences.Get("UserInfo", "{}");
                    userInfo = JsonConvert.DeserializeObject<UserInfo>(data);

                    if (userInfo.status != string.Empty)
                    {
                        return userInfo;
                    }
                }

                return userInfo;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initialized the class.
        /// </summary>
        private UserPrefs()
        {
            if(Preferences.ContainsKey("UserToken"))
            {
                string data = Preferences.Get("UserToken", "{}");
                Debug.WriteLine("BytesCrafter: " + data);
                token = JsonConvert.DeserializeObject<Token>(data);
            }

            if(Preferences.ContainsKey("UserInfo"))
            {
                string data = Preferences.Get("UserInfo", "{}");
                Debug.WriteLine("BytesCrafter: " + data);
                userInfo = JsonConvert.DeserializeObject<UserInfo>(data);
            }
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
