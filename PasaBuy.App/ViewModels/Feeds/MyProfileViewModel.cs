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
            homePostList = new ObservableCollection<Post>();
            Random rnd = new Random();

            try
            {
                SocioPress.Feeds.Instance.Profile(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky,"", PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        try
                        {
                            PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                            Console.WriteLine(data);
                            for (int i = 0; i < post.data.Length; i++)
                            {
                                string post_author = post.data[i].post_author;
                                string id = post.data[i].id;
                                string content = post.data[i].content;
                                string title = post.data[i].title;
                                string date_post = post.data[i].date_post; //date_create change to date_post
                                string type = post.data[i].type;
                                string item_image = post.data[i].item_image;
                                string author = post.data[i].author;
                                string name = post.data[i].name;
                                string views = post.data[i].views;
                                string image = string.Empty;
                                string image_item = string.Empty;
                                if (author != "")
                                {
                                    if (author.Substring(0, PSAConfig.baseRestUrl.Length) != PSAConfig.baseRestUrl)
                                    {
                                        image = PSAConfig.baseRestUrl + author.Substring(PSAConfig.baseRestUrl.Length + 1);
                                    }
                                    else
                                    {
                                        image = author;
                                    }
                                }

                                if (item_image != "")
                                {
                                    if (item_image.Substring(0, PSAConfig.baseRestUrl.Length) != PSAConfig.baseRestUrl)
                                    {
                                        image_item = PSAConfig.baseRestUrl + item_image.Substring(PSAConfig.baseRestUrl.Length + 1);
                                    }
                                    else
                                    {
                                        image_item = item_image;
                                    }
                                }

                                homePostList.Add(new Post(image,
                                    name, type, date_post, views, title, content, image_item));
                            }
                        }
                        catch (Exception)
                        {
                            new Alert("Oops", "Something went wrong in loading data. Please contact administrator", "OK");
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
            homePostList.Clear();
            Random rnd = new Random();
            try
            {
                SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.wpid, (bool success, string data) =>
                {
                    if (success)
                    {
                        try
                        {
                            PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                            for (int i = 0; i < post.data.Length; i++)
                            {
                                string id = post.data[i].id;
                                string name = post.data[i].name;
                                string type = post.data[i].type;
                                string date_post = post.data[i].date_post;
                                string title = post.data[i].title;
                                string content = post.data[i].content;
                                string views = post.data[i].views;
                                string status = post.data[i].status;
                                string pickup_location = post.data[i].pickup_location;
                                string vehicle_type = post.data[i].vehicle_type;
                                string drop_off_location = post.data[i].drop_off_location;
                                string author = post.data[i].author;
                                string item_image = post.data[i].item_image;

                                string image = string.Empty;
                                string image_item = string.Empty;
                                if (author != "")
                                {
                                    if (author.Substring(0, PSAConfig.baseRestUrl.Length) != PSAConfig.baseRestUrl)
                                    {
                                        image = PSAConfig.baseRestUrl + author.Substring(PSAConfig.baseRestUrl.Length + 1);
                                    }
                                    else { image = author; }
                                }

                                if (item_image != "")
                                {
                                    if (item_image.Substring(0, PSAConfig.baseRestUrl.Length) != PSAConfig.baseRestUrl)
                                    {
                                        image_item = PSAConfig.baseRestUrl + item_image.Substring(PSAConfig.baseRestUrl.Length + 1);
                                    }
                                    else { image_item = item_image; }
                                }

                                homePostList.Add(new Post(image,
                                    name, type, date_post, views, title, content, image_item)); //"https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/ArticleImage2.png"

                            }
                        }
                        catch (Exception)
                        {
                            new Alert("Something went Wrong", "Please contact administrator.", "OK");
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
            DateTime date = DateTime.ParseExact(PSACache.Instance.UserInfo.date_registered, "yyyy-MM-dd HH:mm:ss", provider);

            this.BannerImage = PSACache.Instance.UserInfo.bannerUrl;
            this.ProfileImage = PSACache.Instance.UserInfo.avatarUrl;
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
                return PSACache.Instance.UserInfo.avatarUrl;
            }
        }


        #endregion

        #region Methods

        #endregion

    }
}
