using Newtonsoft.Json;
using PasaBuy.App.Controllers;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.Onboarding;
using PasaBuy.App.ViewModels.Menu;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Menu;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
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
        public static string user_id = string.Empty;

        public static ObservableCollection<Post> profilePostList;

        public ObservableCollection<Post> ProfilePosts
        {
            get { return profilePostList; }
            set { profilePostList = value; this.NotifyPropertyChanged(); }
        }

        private string istitle;

        private string bannerImage;

        private string profileImage;

        private Boolean ismessage = false;

        private Boolean isinput = false;

        private Boolean isreferred = false;

        private Boolean iscity = false;

        public static int postcount;

        public static int transactcount;

        public static float ratingscount;

        private static string bannerImages = string.Empty;

        private static string profileImages = string.Empty;

        private static string displayNames = string.Empty;

        private static string verification = string.Empty;

        private static string city = string.Empty;

        private static string joined = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Load post data in listview using observablecollection in MyProfilePage
        /// </summary>
        public static void LoadData(string uid)
        {
            try
            {
                MyProfile.LastIndex = 11;
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky,"", uid, (bool success, string data) =>
                {
                    if (success)
                    {
                        PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                        if (post.data.Length == 0)
                        {
                            profilePostList.Add(new Post(PSAProc.GetUrl("http://localhost/wordpress/wp-content/plugins/SocioPress-WP-Plugin/assets/default-avatar.png"), // PasaBuy.App avatar
                                "Pasabuy.App", "Status", new DateTime().ToString(), "0", "Welcome to Pasabuy.App!", "", "", "-1", "", "", "", "", "", ""));
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
                                string category = post.data[i].item_category;
                                string vehicle_type = post.data[i].vehicle_type;
                                string pickup_location = post.data[i].pickup_location;
                                string do_price = "Drop-off: " + post.data[i].drop_off_location;
                                if (type == "Selling")
                                {
                                    title = category + " : " + title;
                                    do_price = "Price: " + post.data[i].item_price;
                                }

                                profilePostList.Add(new Post(PSAProc.GetUrl(author),
                                    name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link, "", pickup_location, vehicle_type, do_price));
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
        /// Load more post data in listview using observablecollection in HomePage
        /// </summary>
        public static void LoadMore(string lastid)
        {
            try
            {
                if (user_id == string.Empty)
                {
                    user_id = PSACache.Instance.UserInfo.wpid;
                }
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, lastid, user_id, (bool success, string data) =>
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
                            string category = post.data[i].item_category;
                            string vehicle_type = post.data[i].vehicle_type;
                            string pickup_location = post.data[i].pickup_location;
                            string do_price = "Drop-off: " + post.data[i].drop_off_location;
                            if (type == "Selling")
                            {
                                title = category + " : " + title;
                                do_price = "Price: " + post.data[i].item_price;
                            }

                            profilePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link, "", pickup_location, vehicle_type, do_price));
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

        public static void LoadTotal(string userid)
        {
            try
            {
                SocioPress.Posts.Instance.Count(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, userid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        postcount = getdata.data.count;
                    }
                });

                SocioPress.Reviews.Instance.Get(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, userid, (bool success, string data) =>
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
            string userid = string.Empty;
            CultureInfo provider = new CultureInfo("fr-FR");
            if (user_id == string.Empty)
            {
                this.isTitle = "My Profile";
                isMessage = false;
                isInput = true;
                userid = PSACache.Instance.UserInfo.wpid;
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
                this.Refered = "";
                this.Transacts = transactcount;
                this.PostsCount = postcount;
                this.Ratings = ratingscount;
            }
            else
            {
                this.isTitle = "Other Profile";
                isMessage = true;
                isInput = false;
                userid = user_id;
                this.BannerImage = bannerImages;
                this.ProfileImage = profileImages;
                this.DisplayName = displayNames;
                this.Verification = verification;
                if (city != "")
                {
                    isCity = true;
                    this.City = "(ic) Lives in " + city;
                }
                //this.Joined = "(ic) Joined at " + DateTime.Now.ToString("MMMM yyyy");
                this.Joined = "(ic) Joined at " + joined;
                this.Transacts = transactcount;
                this.PostsCount = postcount;
                this.Ratings = ratingscount;
            }

            RefreshCommand = new Command<string>((key) =>
            {
                profilePostList.Clear();
                LoadData(userid);
                IsRefreshing = false;
            });
            profilePostList = new ObservableCollection<Post>();
            LoadData(userid);
        }

        public static void GetProfile(string uid)
        {
            try
            {
                SocioPress.Profile.Instance.GetData(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, uid, (bool success2, string data2) =>
                {
                    if (success2)
                    {
                        UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(data2);

                        if (uinfo.Succeed)
                        {
                            //this.ProfileImage = "https://pasabuy.app/wp-content/plugins/SocioPress/assets/default-avatar.png";
                            bannerImages = PSAProc.GetUrl(uinfo.data.banner);
                            profileImages = PSAProc.GetUrl(uinfo.data.avatar);
                            displayNames = uinfo.data.dname;
                            verification = uinfo.data.verify;
                            Console.WriteLine("City: ." + uinfo.data.city + ".");
                            city = uinfo.data.city == null ? "" : uinfo.data.city;

                            CultureInfo provider = new CultureInfo("fr-FR");
                            DateTime date = DateTime.ParseExact(PSACache.Instance.UserInfo.date_registered == string.Empty ? new DateTime().ToString() : PSACache.Instance.UserInfo.date_registered, "yyyy-MM-dd HH:mm:ss", provider);

                            joined = date.ToString("MMMM yyyy");
                        }

                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data2), "Try Again");
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data2), "Try Again");
                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
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

        #region Property
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
        public Boolean isMessage
        {
            get
            {
                return ismessage;
            }
            set
            {
                ismessage = value;
                this.NotifyPropertyChanged();
            }
        }
        public Boolean isInput
        {
            get
            {
                return isinput;
            }
            set
            {
                isinput = value;
                this.NotifyPropertyChanged();
            }
        }
        public string isTitle
        {
            get
            {
                return istitle;
            }
            set
            {
                istitle = value;
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
