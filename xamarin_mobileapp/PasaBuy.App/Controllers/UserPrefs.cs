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
                string data = JsonConvert.SerializeObject(value);
                Preferences.Set("UserToken", data);
                token = value;
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
                string data = JsonConvert.SerializeObject(value);
                Preferences.Set("UserInfo", data);
                userInfo = value;
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
            
        }

        public bool hasToken
        {
            get
            {
                return token != null ? true : false;
            }
        }

        public bool hasUserinfo
        {
            get
            {
                return userInfo != null ? true : false;
            }
        }

        public void Initialize()
        {
            if (Preferences.ContainsKey("UserToken"))
            {
                string data = Preferences.Get("UserToken", "{}");
                token = JsonConvert.DeserializeObject<Token>(data);
            }

            if (Preferences.ContainsKey("UserInfo"))
            {
                string data = Preferences.Get("UserInfo", "{}");
                userInfo = JsonConvert.DeserializeObject<UserInfo>(data);
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
