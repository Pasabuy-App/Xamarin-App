using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Views.Onboarding;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using DataVice;
using PasaBuy.App.Controllers;
using System;
using PasaBuy.App.Models.Onboarding;

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

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Send button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SendClicked(object obj)
        {
            State = true;
            Users.Instance.Forgot(Email, (bool success, string data) =>
           {
               if (success)
               {
                   State = false;
                   Application.Current.MainPage = new VerifyResetPage();
                   VerifyAccountVar.un = Email;
                   //new Alert("Demoguy Notice", "Forgot Password is Done, proceed to activate account and enter actiavtvion key.", "AGREE");
               }
               else
               {
                   new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");
                   State = false;
               }
           });
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            App.Current.MainPage.Navigation.PopModalAsync();
        }

        #endregion
    }
}