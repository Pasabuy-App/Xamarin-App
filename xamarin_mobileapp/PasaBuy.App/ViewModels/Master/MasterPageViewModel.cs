﻿using PasaBuy.App.Views.Master;
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

namespace PasaBuy.App.ViewModels.Master
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class MasterPageViewModel : BaseViewModel
    {
        #region Fields

        private string profileName;

        private string profileImage;

        private string userBanner;

        private string email;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MasterPageViewModel" /> class.
        /// </summary>
        public MasterPageViewModel()
        {
            this.profileName = UserPrefs.Instance.UserInfo.dname;
            this.profileImage = UserPrefs.Instance.UserInfo.avatarUrl;
            this.userBanner = UserPrefs.Instance.UserInfo.bannerUrl;
            this.email = UserPrefs.Instance.UserInfo.email;

            this.HomeCommand = new Command(this.HomeButtonClicked);
            this.ProfileCommand = new Command(this.ProfileButtonClicked);
            this.MessageCommand = new Command(this.MessageButtonClicked);
            this.StoreCommand = new Command(this.StoreButtonClicked);
            this.UpdatesCommand = new Command(this.UpdatesButtonClicked);
            this.NotificationCommand = new Command(this.NotificationButtonClicked);
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
        /// Gets or sets the command that is executed when the home view is clicked.
        /// </summary>
        public Command HomeCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile view is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the message view is clicked.
        /// </summary>
        public Command MessageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the mystore view is clicked.
        /// </summary>
        public Command StoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command UpdatesCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the request view is clicked.
        /// </summary>
        public Command SettingCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the notification view is clicked.
        /// </summary>
        public Command NotificationCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the home button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void HomeButtonClicked(object obj)
        {
            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new DetailPage());
        }

        /// <summary>
        /// Invoked when the profile button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProfileButtonClicked(object obj)
        {
            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new MyProfile());
        }

        /// <summary>
        /// Invoked when the message button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MessageButtonClicked(object obj)
        {
            //((MainPage)App.Current.MainPage).Detail = new NavigationPage(new RecentChatPage());
        }

        /// <summary>
        /// Invoked when the mystore button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void StoreButtonClicked(object obj)
        {
            //((MainPage)App.Current.MainPage).Detail = new NavigationPage(new StoreBrowserPage());
        }

        /// <summary>
        /// Invoked when the request button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void UpdatesButtonClicked(object obj)
        {
            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new ArticleList());
        }

        /// <summary>
        /// Invoked when the notification button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void NotificationButtonClicked(object obj)
        {
            ((MainPage)App.Current.MainPage).Detail = new NavigationPage(new TaskNotificationPage());
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
