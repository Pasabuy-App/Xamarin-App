using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Onboarding;
using System;
using Xamarin.Forms;
using static PasaBuy.App.Models.Onboarding.VerifyAccountVar;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class VerifyAccountViewModel : BaseViewModel
    {

        #region Fields
        public static string user_name;
        private string ak;

        private string un;

        private Boolean _state = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="VerifyAccountViewModel" /> class.
        /// </summary>
        public VerifyAccountViewModel()
        {
            this.SubmitCommand = new Command(this.SfButton_Clicked);
            State = false;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the username from user in the Verify Account page.
        /// </summary>
        public string Username
        {
            get
            {
                return this.un;
            }

            set
            {
                if (this.un == value)
                {
                    return;
                }

                this.un = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the activation key from users in the Verify Account page.
        /// </summary>
        public string ActivationKey
        {
            get
            {
                return this.ak;
            }

            set
            {
                if (this.ak == value)
                {
                    return;
                }

                this.ak = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the Verify Account page.
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
        /// Gets or sets the command that is executed when the Verify Account button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Verify Account button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>


        private void SfButton_Clicked(object obj)
        {
            try
            {
                if (!State)
                {
                    State = true;
                    Http.DataVice.Users.Instance.Activate(ActivationKey, user_name, (bool success, string data) =>
                    {
                        if (success)
                        {
                            VerifyAccountData akey = JsonConvert.DeserializeObject<VerifyAccountData>(data);
                            State = false;

                            VerifyAccountVar.un = user_name;
                            VerifyAccountVar.ak = akey.key;
                            Application.Current.MainPage = new CreatePassword();
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            State = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-AC1VAVM.", "OK");
            }
        }


        #endregion
    }
}
