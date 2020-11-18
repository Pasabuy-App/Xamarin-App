using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.Views;
using PasaBuy.App.Views.Onboarding;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Onboarding
{
    public class CreatePasswordViewModel : BaseViewModel
    {

        #region Fields

        private string act_key;

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
        /// Activation Key for User Verification.
        /// </summary>
        public string ActKey
        {
            get
            {
                return this.act_key;
            }

            set
            {
                this.act_key = value;
                this.NotifyPropertyChanged();
            }
        }

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
                    Http.DataVice.Users.Instance.NewPassword(ActKey, Password, ConfirmPassowrd, async(bool success1, string data1) =>
                    {
                        if (success1)
                        {
                            
                            App.Current.MainPage = new SignInPage();
                            await App.Current.MainPage.Navigation.PushModalAsync(new NoticePage());

                            //bool answer = await Application.Current.MainPage.DisplayAlert(
                            //    "Verify Account?", "Congratulations! Thank you for downloading PasaBuy.App. " +
                            //    "Ikaw ay makakatanggap ng Php500 sa iyong E-wallet. Sa ngayon ay " +
                            //    "hindi pa magagamit ang App ngunit maaari ka ng mag-verify ng account. " +
                            //    "Verify mo lang ang account mo at makakatanggap ka pa ng dagdag na Php300.", 
                            //    "Confirm", "No, Later");



                            //if (answer)
                            //{
                            //    //Check if user is Verified or Not.
                            //    await PopupNavigation.Instance.PushAsync(new PopupStartup());
                            //}
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