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
            Users.Instance.Auth(Email, Password, (bool success, string data) =>
            {
                if (success)
                {
                    Token token  = JsonConvert.DeserializeObject<Token>(data);
                    Users.Instance.Profile(token.data.wpid, token.data.snky, (bool success2, string data2) =>
                    {
                        if (success2)
                        {
                            UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data2);

                            if (uinfo.status == "success")
                            {
                                //UserPrefs.Instance.UserInfo = uinfo;
                                UserPrefs.Instance.UserInfo.dname = uinfo.data.dname;
                                UserPrefs.Instance.UserInfo.uname = uinfo.data.uname;
                                UserPrefs.Instance.UserInfo.email = uinfo.data.email;
                                UserPrefs.Instance.UserInfo.city = uinfo.data.city;
                                UserPrefs.Instance.UserInfo.email = uinfo.data.email;
                                UserPrefs.Instance.UserInfo.city = uinfo.data.city;
                                UserPrefs.Instance.UserInfo.lname = uinfo.data.lname;
                                UserPrefs.Instance.UserInfo.fname = uinfo.data.fname;
                                //Console.WriteLine(UserPrefs.Instance.UserInfo.dname + " display name");

                                Application.Current.MainPage = new Views.MainTabs();
                            }

                            else
                            {
                                new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data2), "Try Again");
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });

            /*RestAPI.Instance.Authenticate(Email, Password, (bool success, string data) =>
            {
                if(success)
                {
                    UserPrefs.Instance.Token = JsonConvert.DeserializeObject<Token>(data);

                    RestAPI.Instance.GetUserInfo(UserPrefs.Instance.Token, (bool success2, string data2) =>
                    {
                        if (success2)
                        {
                            UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data2);

                            if (uinfo.status == "success")
                            {
                                UserPrefs.Instance.UserInfo = uinfo;

                                Application.Current.MainPage = new PasaBuy.App.Views.MainTabs();
                            }

                            else
                            {
                                new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data2), "Try Again");
                            }
                        }

                        else 
                        {
                            new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data2), "Try Again");
                        }
                        
                    });
                }

                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                }
            });*/
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
