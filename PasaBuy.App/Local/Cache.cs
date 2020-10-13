using Newtonsoft.Json;
using PasaBuy.App.Models.Onboarding;
using Xamarin.Essentials;

namespace PasaBuy.App.Local
{
    /// <summary>
    /// Global access of user preferences.
    /// </summary>
    public class PSACache
    {
        #region Fields
        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static PSACache instance;
        public static PSACache Instance
        {
            get
            {
                if (instance == null)
                    instance = new PSACache();
                return instance;
            }
        }

        /// <summary>
        /// Store Primary user information on memory.
        /// by Preferences
        /// </summary>
        private UserInfo userInfo;

        public void SaveUserData()
        {
            string data = JsonConvert.SerializeObject(userInfo);
            Preferences.Set("UserInfo", data);
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
        /// Responsible for checking and getting user prefs.
        /// </summary>
        public UserInfo UserInfo
        {
            set
            {
                userInfo = value;
            }

            get
            {
                return userInfo;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initialized the class.
        /// </summary>
        private PSACache()
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

                /*RestAPI.Instance.GetUserInfo(token, (bool success, string info) =>
                {
                    if (success)
                    {
                        UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(info);

                        if (uinfo.Succeed)
                        {
                            UserInfo = uinfo;
                        }
                    }
                });*/
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check if the user skip or completed the Getting Started.
        /// </summary>
        public static bool DoneWithGettingStarted
        {
            get
            {
                return Preferences.ContainsKey("ReturnUser") ? true : false;
            }
        }

        /// <summary>
        /// Set and save the action of the user toward the Getting Started page.
        /// </summary>
        /// <param name="isSkipped"></param>
        public static void SetGettingStartedAction(bool isSkipped)
        {
            Preferences.Set("ReturnUser", true);
        }
        #endregion
    }
}
