using PasaBuy.App.Views.Master;
using PasaBuy.App.Views.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Essentials;
using PasaBuy.App.Views.Onboarding;
using PasaBuy.App.Views;
using System;
using System.Threading.Tasks;
using PasaBuy.App.Local;

namespace PasaBuy.App.ViewModels.Settings
{
    /// <summary>
    /// ViewModel for Setting page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class SettingViewModel : BaseViewModel
    {
        private bool isEnable = false;
        bool _isVisible = false;
        public bool isVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingViewModel" /> class
        /// </summary>
        public SettingViewModel()
        {
            isVisible = true;
            /*if (PSACache.Instance.UserInfo.verify != "Verified")
            {
                isVisible = true;
            }
            else
            {
                isVisible = false;
            }*/
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.MyAddressCommand = new Command(this.MyAddressClicked);
            this.MyContactCommand = new Command(this.MyContactClicked);
            //this.MyTransactionsCommand = new Command(this.MyTransactionsClicked);
            this.ChangePasswordCommand = new Command(this.ChangePasswordClicked);
            this.VerifyAccountCommand = new Command(this.VerifyAccountClicked);

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
        /// Gets or sets the command is executed when the my contact option is clicked.
        /// </summary>
        public Command MyContactCommand { get; set; }

        public Command MyTransactionsCommand { get; set; }


        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command ChangePasswordCommand { get; set; }
        public Command VerifyAccountCommand { get; set; }

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
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new EditProfilePage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new EditProfilePage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void MyAddressClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddressPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new AddressPage()));
                    isEnable = false;
                });
            }
        }

        private void MyContactClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ContactPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ContactPage()));
                    isEnable = false;
                });
            }
        }

        /*private void MyTransactionsClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyTransactionsPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyTransactionsPage()));
                    isEnable = false;
                });
            }
        }*/

        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void ChangePasswordClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ChangePWPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ChangePWPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new LinkedPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new LinkedPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void TermsServiceClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new TermsPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new TermsPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void PrivacyPolicyClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PrivacyPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new PrivacyPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        /// 
        private void FAQClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new FAQPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new FAQPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new HelpPage()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new HelpPage()));
                    isEnable = false;
                });
            }
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LogoutClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Delay(100);
                    Application.Current.MainPage = new NavigationPage(new SignInPage());
                    Preferences.Remove("UserInfo");
                    isEnable = false;
                });
            }
        }

        private void VerifyAccountClicked(object obj)
        {
            if (!isEnable)
            {
                isEnable = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new VerifyAccountFront()));
                    await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new VerifyAccountFront()));
                    isEnable = false;
                });
            }
        }


        #endregion
    }
}
