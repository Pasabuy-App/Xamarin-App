using PasaBuy.App.Views.Onboarding;
using PasaBuy.App.Views.Master;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Diagnostics;
using System;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Controllers;
using Newtonsoft.Json;
using PasaBuy.App.Models.Onboarding;
using DataVice;
using PasaBuy.App.Local;

namespace PasaBuy.App.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for login with social icon page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignInViewModel : LoginViewModel
    {
        #region Fields

        private string password;

        private Boolean _state = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignInViewModel" /> class.
        /// </summary>
        public SignInViewModel()
        {
            
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.FaceBookLoginCommand = new Command(this.FaceBookClicked);
            this.TwitterLoginCommand = new Command(this.TwitterClicked);
            this.GmailLoginCommand = new Command(this.GmailClicked);
       
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the login page.
        /// </summary>
        public Boolean State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the facebook login button is clicked.
        /// </summary>
        public Command FaceBookLoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the twitter login button is clicked.
        /// </summary>
        public Command TwitterLoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the gmail login button is clicked.
        /// </summary>
        public Command GmailLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoginClicked(object obj)
        {
            try
            {
                State = true;
                Users.Instance.Auth(Email, Password, (bool success, string data) =>
                {
                    if (success)
                    {
                        Token token = JsonConvert.DeserializeObject<Token>(data);

                        if (!token.isSuccess)
                        {
                            new Alert("Something went Wrong", HtmlUtils.ConvertToPlainText(token.message), "OK");
                            return;
                        }

                        //Initialized the class first.
                        PSACache.Instance.UserInfo = new UserInfo();

                        //Store User token on Cache
                        PSACache.Instance.UserInfo.wpid = token.data.wpid;
                        PSACache.Instance.UserInfo.snky = token.data.snky;

                        PSACache.Instance.SaveUserData();

                        //Reuqest user token after device received token.
                        SocioPress.Profile.Instance.GetData(token.data.wpid, token.data.snky, token.data.wpid, (bool success2, string data2) =>
                        {
                            if (success2)
                            {
                                UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data2);

                                /*Debug.WriteLine("Demoguy1: " + data2);
                                Debug.WriteLine("Demoguy2: " + JsonConvert.SerializeObject(uinfo));*/

                                if (uinfo.Succeed)
                                {

                                    PSACache.Instance.UserInfo.dname = uinfo.data.dname;
                                    PSACache.Instance.UserInfo.uname = uinfo.data.uname;
                                    PSACache.Instance.UserInfo.email = uinfo.data.email;
                                    PSACache.Instance.UserInfo.lname = uinfo.data.lname;
                                    PSACache.Instance.UserInfo.fname = uinfo.data.fname;
                                    PSACache.Instance.UserInfo.city = uinfo.data.city;
                                    PSACache.Instance.UserInfo.date_registered = uinfo.data.date_registered;
                                    PSACache.Instance.UserInfo.avatar = uinfo.data.avatar;
                                    PSACache.Instance.UserInfo.banner = uinfo.data.banner;
                                    PSACache.Instance.UserInfo.verify = uinfo.data.verify;

                                    State = false;
                                    Application.Current.MainPage = new Views.MainTabs();
                                    PSACache.Instance.SaveUserData();
                                }

                                else
                                {
                                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data2), "Try Again");
                                    State = false;
                                }
                            }

                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                State = false;
                            }
                        });
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        State = false;
                    }
                });
            }

            catch (Exception ex)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
                Console.WriteLine("Error: " + ex);
            }
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new SignUpPage()));
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ForgotPasswordClicked(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ForgotPwPage()));
        }

        /// <summary>
        /// Invoked when facebook login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void FaceBookClicked(object obj)
        {
            Launcher.OpenAsync("http://facebook.com");

        }

        /// <summary>
        /// Invoked when twitter login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void TwitterClicked(object obj)
        {
            Launcher.OpenAsync("http://twitter.com");
        }

        /// <summary>
        /// Invoked when gmail login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void GmailClicked(object obj)
        {
            Launcher.OpenAsync("http://google.com");
        }

        #endregion
    }
}
