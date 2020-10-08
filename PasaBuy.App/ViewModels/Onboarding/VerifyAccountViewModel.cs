using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using DataVice;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Controllers;
using PasaBuy.App.Views.Onboarding;
using Newtonsoft.Json;
using static PasaBuy.App.Models.Onboarding.VerifyAccountVar;
using PasaBuy.App.Local;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class VerifyAccountViewModel : BaseViewModel
    {

        #region Fields

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
                    Users.Instance.Activate(ActivationKey, Username, (bool success, string data) =>
                    {
                        if (success)
                        {
                            VerifyAccountData akey = JsonConvert.DeserializeObject<VerifyAccountData>(data);
                            State = false;
                            VerifyAccountVar.un = Username;
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }


        #endregion
    }
}
