using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Feeds
{
    /// <summary>
    /// ViewModel for social profile pages.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class MyProfileViewModel : BaseViewModel
    {
        #region Fields

        private string bannerImage;

        private string profileImage;

        private Boolean isreferred = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MyProfileViewModel" /> class.
        /// </summary>
        public MyProfileViewModel()
        {
            CultureInfo provider = new CultureInfo("fr-FR");
            DateTime date = DateTime.ParseExact(PSACache.Instance.UserInfo.date_registered, "yyyy-MM-dd HH:mm:ss", provider);

            this.BannerImage = PSACache.Instance.UserInfo.bannerUrl;
            this.ProfileImage = PSACache.Instance.UserInfo.avatarUrl;
            this.DisplayName = PSACache.Instance.UserInfo.dname;
            this.Verification = PSACache.Instance.UserInfo.verify;

            this.City = "(ic) Lives in " + PSACache.Instance.UserInfo.city;
            this.Joined = "(ic) Joined at " + date.ToString("MMMM yyyy");
            isRefered = false;
            this.Refered = "";// "(ic) Refered by " + UserPrefs.Instance.UserInfo.city;
                                              //Joined
                                              //Refered
            this.Transacts = ProfileGetData.totaltransact;
            this.Ratings = ProfileGetData.totalratings;
            this.PostsCount = ProfileGetData.totalpost;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the menu back button is clicked.
        /// </summary>
        public ICommand MenuBackCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Follow button is clicked.
        /// </summary>
        public ICommand FollowCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the message button is clicked.
        /// </summary>
        public ICommand MessageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the AddConnection button is clicked.
        /// </summary>
        public ICommand AddConnectionCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Image is tapped.
        /// </summary>
        public ICommand ImageTapCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile item is tapped.
        /// </summary>
        public ICommand ProfileSelectedCommand { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the header image url.
        /// </summary>
        public string BannerImage
        {
            get { return this.bannerImage; }
            set { this.bannerImage = value; }
        }

        /// <summary>
        /// Gets or sets the profile image url.
        /// </summary>
        public string ProfileImage
        {
            get { return this.profileImage; }
            set { this.profileImage = value; }
        }

        /// <summary>
        /// Gets or sets the profile name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the verification status.
        /// </summary>
        public string Verification { get; set; }

        /// <summary>
        /// Gets or sets the City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Joined
        /// </summary>
        public string Joined { get; set; }

        /// <summary>
        /// Gets or sets the Refered
        /// </summary>
        public string Refered { get; set; }

        /// <summary>
        /// Gets or sets the posts count
        /// </summary>
        public int PostsCount { get; set; }

        /// <summary>
        /// Gets or sets the transaction number
        /// </summary>
        public int Transacts { get; set; }

        /// <summary>
        /// Gets or sets the ratings
        /// </summary>
        public float Ratings { get; set; }

        /// <summary>
        /// Gets or sets the property that is bound with label that gets the visibility of referred from user in the myprofile page.
        /// </summary>
        public Boolean isRefered
        {
            get
            {
                return isreferred;
            }
            set
            {
                isreferred = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        #endregion

    }
}
