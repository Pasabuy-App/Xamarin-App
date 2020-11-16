using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views.Onboarding;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for forgot password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ForgotPwViewModel : LoginViewModel
    {

        private Boolean _state = false;

        /// <summary>
        /// Gets or sets the property that is bound with stacklayout that gets the visibility state from user in the forgot page.
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

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPwViewModel" /> class.
        /// </summary>
        public ForgotPwViewModel()
        {
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.SendCommand = new Command(this.SendClicked);
            this.ActivateNowCommand = new Command(this.ActivateNowClicked);
            State = false;
        }

        

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        public Command ActivateNowCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SendClicked(object obj)
        {
            try
            {
                if (!State)
                {
                    State = true;
                    Http.DataVice.Users.Instance.Forgot(Email, (bool success, string data) =>
                    {
                        if (success)
                        {
                            State = false;
                            Application.Current.MainPage = new CreatePassword();
                            VerifyAccountVar.un = Email;
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-F1FPVM.", "OK");
            }
        }

        private void ActivateNowClicked(object obj)
        {
            State = true;
            App.Current.MainPage.Navigation.PushModalAsync(new VerifyAccountPage());
            State = false;
        }
        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        #endregion
    }
}