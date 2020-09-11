using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Menu;
using PasaBuy.App.Views.Menu;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
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

        public static ObservableCollection<Post> profilePostList;

        public ObservableCollection<Post> ProfilePosts
        {
            get { return profilePostList; }
            set { profilePostList = value; this.NotifyPropertyChanged(); }
        }

        private string bannerImage;

        private string profileImage;

        private Boolean isreferred = false;

        private Boolean iscity = false;

        public static int postcount;

        public static int transactcount;

        public static float ratingscount;

        #endregion

        #region Constructor

        /// <summary>
        /// Load post data in listview using observablecollection in MyProfilePage
        /// </summary>
        public static void LoadData()
        {
            try
            {
                profilePostList = new ObservableCollection<Post>();
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky,"", PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                        if (post.data.Length == 0)
                        {
                            profilePostList.Add(new Post(PSAProc.GetUrl("http://localhost/wordpress/wp-content/plugins/SocioPress-WP-Plugin/assets/default-avatar.png"), // PasaBuy.App avatar
                                "Pasabuy.App", "Status", new DateTime().ToString(), "0", "Welcome to Pasabuy.App!", "", "", "-1", "", ""));
                        }
                        else
                        {
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
                                string post_link = post.data[i].post_link;

                                profilePostList.Add(new Post(PSAProc.GetUrl(author),
                                    name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link));
                            }
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
        /// Refresh post data in listview using observablecollection in MyProfilePage
        /// </summary>
        public static void RefreshList()
        {
            try
            {
                profilePostList.Clear();
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
                            string post_link = post.data[i].post_link;

                            profilePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link));
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
        /// Load more post data in listview using observablecollection in HomePage
        /// </summary>
        public static void LoadMore(string lastid)
        {
            try
            {
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, lastid, PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
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
                            string post_link = post.data[i].post_link;

                            profilePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link));
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
                LoadTotal();
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator - HP Refresh.", "OK");
            }
        }

        public static void LoadTotal()
        {
            try
            {
                SocioPress.Posts.Instance.Count(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        postcount = getdata.data.count;
                    }
                });

                SocioPress.Reviews.Instance.Get(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        ratingscount = getdata.data.ave_rating;
                    }
                });
                SocioPress.Transaction.Instance.GetTotal(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        transactcount = getdata.data.transac;
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
            RefreshCommand = new Command<string>((key) =>
            {
                RefreshList();
                LoadTotal();
                IsRefreshing = false;
            });
            //LoadTotal();
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
            this.Transacts = transactcount; // PSACache.Instance.UserInfo.totaltransact;//PSACache.Instance.UserInfo.totaltransact; 
            this.Ratings = ratingscount; //PSACache.Instance.UserInfo.rating; //ProfileGetData.totalratings;
            this.PostsCount = postcount; //PSACache.Instance.UserInfo.countpost; // ProfileGetData.totalpost;
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

        #region Propertiy
        public ICommand RefreshCommand { protected set; get; }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }

            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();

                }
            }
        }


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
