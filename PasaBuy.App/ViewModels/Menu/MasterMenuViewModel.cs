using PasaBuy.App.Views.Master;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using PasaBuy.App.Views.Notification;
using PasaBuy.App.Views.Settings;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Marketplace;
using System.Linq;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Advisory;
using PasaBuy.App.Controllers;
using PasaBuy.App.Views;
using PasaBuy.App.Views.Currency;
using PasaBuy.App.Views.Backend;
using PasaBuy.App.Local;

namespace PasaBuy.App.ViewModels.Menu
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MasterMenuViewModel : BaseViewModel
    {
        #region Fields

        private string profileName;

        private string profileImage;

        private string userBanner;

        private string email;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MasterMenuViewModel" /> class.
        /// </summary>
        public MasterMenuViewModel()
        {
            this.profileName = PSACache.Instance.UserInfo.dname;
            this.profileImage = PSACache.Instance.UserInfo.avatarUrl;
            this.userBanner = PSACache.Instance.UserInfo.bannerUrl;
            this.email = PSACache.Instance.UserInfo.email;

            this.ProfileCommand = new Command(this.ProfileButtonClicked);
            this.WalletCommand = new Command(this.WalletButtonClicked);
            this.AdvisoryCommand = new Command(this.AdvisoryButtonClicked);
            this.NotificationCommand = new Command(this.NotificationButtonClicked);
            this.DriverCommand = new Command(this.DriverButtonClicked);
            this.StoreCommand = new Command(this.StoreButtonClicked);
            this.SettingCommand = new Command(this.SettingButtonClicked);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string UserFullname
        {
            get
            {
                return this.profileName;
            }

            set
            {
                if (this.profileName != value)
                {
                    this.profileName = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string UserPhoto
        {
            get
            {
                return this.profileImage;
            }

            set
            {
                if (this.profileImage != value)
                {
                    this.profileImage = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user banner.
        /// </summary>
        public string UserBanner
        {
            get
            {
                return this.userBanner;
            }

            set
            {
                if (this.userBanner != value)
                {
                    this.userBanner = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string UserEmail
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the profile view is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the wallet view is clicked.
        /// </summary>
        public Command WalletCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the notification view is clicked.
        /// </summary>
        public Command NotificationCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command AdvisoryCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the driver view is clicked.
        /// </summary>
        public Command DriverCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the store view is clicked.
        /// </summary>
        public Command StoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command SettingCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileButtonClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new MyProfile()));
        }

        /// <summary>
        /// Invoked when the wallet button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void WalletButtonClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new WalletPage()));
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void NotificationButtonClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new TaskNotificationPage()));
        }

        /// <summary>
        /// Invoked when the request button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void AdvisoryButtonClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ArticleList()));
        }

        /// <summary>
        /// Invoked when the driver button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void DriverButtonClicked(object obj)
        {
            App.Current.MainPage = new DriverMain();
        }

        /// <summary>
        /// Invoked when the store button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void StoreButtonClicked(object obj)
        {
            App.Current.MainPage = new StoreMain();
        }

        /// <summary>
        /// Invoked when the setting button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void SettingButtonClicked(object obj)
        {
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new SettingPage()));
        }
        #endregion
    }
}
