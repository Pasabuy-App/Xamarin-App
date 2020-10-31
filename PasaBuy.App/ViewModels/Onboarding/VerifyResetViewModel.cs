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
    public class VerifyResetViewModel : BaseViewModel
    {

        #region Fields

        private string ak;

        private Boolean _state = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="VerifyResetViewModel" /> class.
        /// </summary>
        public VerifyResetViewModel()
        {
            this.SubmitCommand = new Command(this.SfButton_Clicked);
            State = false;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the activation key from users in Verify Reset page.
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
                    Http.DataVice.Users.Instance.Activate(ActivationKey, VerifyAccountVar.un, (bool success, string data) =>
                    {
                        if (success)
                        {
                            State = false;
                            VerifyAccountData akey = JsonConvert.DeserializeObject<VerifyAccountData>(data);
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
