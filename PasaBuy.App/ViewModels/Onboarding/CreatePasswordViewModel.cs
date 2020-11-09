using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class CreatePasswordViewModel : BaseViewModel
    {

        #region Fields

        private string new_pw;

        private string confirm_pw;

        private Boolean _state = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="VerifyResetViewModel" /> class.
        /// </summary>
        public CreatePasswordViewModel()
        {
            this.SubmitCommand = new Command(this.SfButton_Clicked);
            State = false;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the activation key from users in Verify Reset page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.new_pw;
            }

            set
            {
                if (this.new_pw == value)
                {
                    return;
                }

                this.new_pw = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the activation key from users in Verify Reset page.
        /// </summary>
        public string ConfirmPassowrd
        {
            get
            {
                return this.confirm_pw;
            }

            set
            {
                if (this.confirm_pw == value)
                {
                    return;
                }

                this.confirm_pw = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the Verify Reset  page.
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
        /// Gets or sets the command that is executed when the Verify Reset button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>


        private void SfButton_Clicked(object obj)
        {
            try
            {
                if (!State)
                {
                    State = true;
                    Http.DataVice.Users.Instance.NewPassword(VerifyAccountVar.ak, VerifyAccountVar.un, Password, ConfirmPassowrd, (bool success1, string data1) =>
                    {
                        if (success1)
                        {
                            Http.DataVice.Users.Instance.Auth(VerifyAccountVar.un, Password, (bool success, string data) =>
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
                                    Http.SocioPress.Profile.Instance.Data( token.data.wpid, (bool success2, string data2) =>
                                    {
                                        if (success2)
                                        {
                                            UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data2);

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
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data1), "Try Again");
                            State = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-A1CP.", "OK");
            }
        }
        #endregion
    }
}