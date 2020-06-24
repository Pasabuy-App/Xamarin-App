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
using PasaBuy.App.Views.Currency;
using PasaBuy.App.Views.Backend;

namespace PasaBuy.App.ViewModels.Menu
{
    /// <summary>
    /// ViewModel for burger menu expand page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class StoreMenuViewModel : BaseViewModel
    {
        #region Fields

        private string profileName;

        private string profileImage;

        private string userBanner;

        private string email;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="StoreMenuViewModel" /> class.
        /// </summary>
        public StoreMenuViewModel()
        {
            this.profileName = UserPrefs.Instance.UserInfo.dname;
            this.profileImage = UserPrefs.Instance.UserInfo.avatarUrl;
            this.userBanner = UserPrefs.Instance.UserInfo.bannerUrl;
            this.email = UserPrefs.Instance.UserInfo.email;

            this.DashboardCommand = new Command(this.DashboardButtonClicked);
            this.StorefrontCommand = new Command(this.StorefrontButtonClicked);
            this.ProductsCommand = new Command(this.ProductButtonClicked);
            this.CategoryCommand = new Command(this.CategoryButtonClicked);
            this.TransactionCommand = new Command(this.TransactionButtonClicked);
            this.MessageCommand = new Command(this.MessageButtonClicked);
            this.VoucherCommand = new Command(this.VoucherButtonClicked);
            this.DocumentCommand = new Command(this.DocumentButtonClicked);
            this.HomepageCommand = new Command(this.HomepageButtonClicked);
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
        /// Gets or sets the command that is executed when the dashboard view is clicked.
        /// </summary>
        public Command DashboardCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the storefront view is clicked.
        /// </summary>
        public Command StorefrontCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the products view is clicked.
        /// </summary>
        public Command ProductsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the category view is clicked.
        /// </summary>
        public Command CategoryCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the transaction view is clicked.
        /// </summary>
        public Command TransactionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the message view is clicked.
        /// </summary>
        public Command MessageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the voucher view is clicked.
        /// </summary>
        public Command VoucherCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the driver view is clicked.
        /// </summary>
        public Command DocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the homepage view is clicked.
        /// </summary>
        public Command HomepageCommand { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the dashboard button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void DashboardButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the storefront button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void StorefrontButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the product button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ProductButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the category button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void CategoryButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the transaction button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void TransactionButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the message button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MessageButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the voucher button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VoucherButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the document button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void DocumentButtonClicked(object obj)
        {
            //Do something
        }

        /// <summary>
        /// Invoked when the homepage button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void HomepageButtonClicked(object obj)
        {
            App.Current.MainPage = new MainTabs();
        }
        #endregion
    }
}