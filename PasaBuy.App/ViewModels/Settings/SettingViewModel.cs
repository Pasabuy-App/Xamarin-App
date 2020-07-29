using PasaBuy.App.Views.Master;
using PasaBuy.App.Views.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Essentials;
using PasaBuy.App.Views.Onboarding;
using PasaBuy.App.Views;

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for Setting page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class SettingViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingViewModel" /> class
        /// </summary>
        public SettingViewModel()
        {
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.MyAddressCommand = new Command(this.MyAddressClicked);
            this.ChangePasswordCommand = new Command(this.ChangePasswordClicked);
            this.LinkAccountCommand = new Command(this.LinkAccountClicked);
            this.HelpCommand = new Command(this.HelpClicked);
            this.TermsCommand = new Command(this.TermsServiceClicked);
            this.PolicyCommand = new Command(this.PrivacyPolicyClicked);
            this.FAQCommand = new Command(this.FAQClicked);
            this.LogoutCommand = new Command(this.LogoutClicked);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command is executed when the edit profile option is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the my address option is clicked.
        /// </summary>
        public Command MyAddressCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command ChangePasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the account link option is clicked.
        /// </summary>
        public Command LinkAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the help option is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the terms of service option is clicked.
        /// </summary>
        public Command TermsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the privacy policy option is clicked.
        /// </summary>
        public Command PolicyCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the FAQ option is clicked.
        /// </summary>
        public Command FAQCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the logout option is clicked.
        /// </summary>
        public Command LogoutCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditProfileClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new EditProfilePage()));
        }

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MyAddressClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddressPage()));
        }

        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void ChangePasswordClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ChangePWPage()));
        }

        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new LinkedPage()));
        }

        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void TermsServiceClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new TermsPage()));
        }

        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void PrivacyPolicyClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PrivacyPage()));
        }

        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        /// 
        private void FAQClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new FAQPage()));
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new HelpPage()));
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LogoutClicked(object obj)
        {
            Application.Current.MainPage = new NavigationPage(new SignInPage());

            Preferences.Remove("UserToken");
        }

        #endregion
    }
}
