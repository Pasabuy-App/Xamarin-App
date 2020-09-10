using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using System;
using System.Collections.ObjectModel;
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

        public static ObservableCollection<Post> homePostList;

        private string bannerImage;

        private string profileImage;

        private Boolean isreferred = false;

        private Boolean iscity = false;

        #endregion

        #region Constructor

        public ObservableCollection<Post> HomePosts
        {
            get { return homePostList; }
            set { homePostList = value; this.NotifyPropertyChanged(); }
        }

        public static void LoadData()
        {
            try
            {
                homePostList = new ObservableCollection<Post>();
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky,"", PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                        
                        for (int i = 0; i < post.data.Length; i++)
                        {
                            string image_height = "-1";
                            if (post.data[i].item_image != "")
                            {
                                image_height = "400";
                            }
                            string post_author = post.data[i].post_author;
                            string id = post.data[i].id;
                            string content = post.data[i].content;
                            string title = post.data[i].title;
                            string date_post = post.data[i].date_post == string.Empty ? new DateTime().ToString() : post.data[i].date_post;
                            string type = post.data[i].type;
                            string item_image = post.data[i].item_image;
                            string author = post.data[i].author;
                            string name = post.data[i].name;
                            string views = post.data[i].views;

                            homePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height));
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        public static void RefreshList()
        {
            try
            {
                homePostList.Clear();
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                        
                        for (int i = 0; i < post.data.Length; i++)
                        {
                            string image_height = "-1";
                            if (post.data[i].item_image != "")
                            {
                                image_height = "400";
                            }
                            string post_author = post.data[i].post_author;
                            string id = post.data[i].id;
                            string content = post.data[i].content;
                            string title = post.data[i].title;
                            string date_post = post.data[i].date_post == string.Empty ? new DateTime().ToString() : post.data[i].date_post;
                            string type = post.data[i].type;
                            string item_image = post.data[i].item_image;
                            string author = post.data[i].author;
                            string name = post.data[i].name;
                            string views = post.data[i].views;

                            homePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height));
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        /// <summary>
        /// Initializes a new instance for the <see cref="MyProfileViewModel" /> class.
        /// </summary>
        public MyProfileViewModel()
        {
            LoadData();
            CultureInfo provider = new CultureInfo("fr-FR");
            DateTime date = DateTime.ParseExact(PSACache.Instance.UserInfo.date_registered == string.Empty ? new DateTime().ToString() : PSACache.Instance.UserInfo.date_registered, "yyyy-MM-dd HH:mm:ss", provider);

            this.BannerImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.bannerUrl);
            this.ProfileImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
            this.DisplayName = PSACache.Instance.UserInfo.dname;
            this.Verification = PSACache.Instance.UserInfo.verify;
            if (PSACache.Instance.UserInfo.city != "")
            {
                isCity = true;
                this.City = "(ic) Lives in " + PSACache.Instance.UserInfo.city;
            }
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

        /// <summary>
        /// Gets or sets the property that is bound with label that gets the visibility of joined from user in the myprofile page.
        /// </summary>
        public Boolean isCity
        {
            get
            {
                return iscity;
            }
            set
            {
                iscity = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Photo
        {
            get
            {
                return PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
            }
        }


        #endregion

        #region Methods

        #endregion

    }
}
