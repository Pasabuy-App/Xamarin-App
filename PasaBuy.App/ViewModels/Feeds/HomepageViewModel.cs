using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Feeds;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.Menu;
using Plugin.Media.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Feeds
{
    public class HomepageViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<Post> homePostList;
        public ObservableCollection<Post> HomePosts
        {
            get 
            { 
                return homePostList; 
            }
            set 
            { 
                homePostList = value; 
                this.NotifyPropertyChanged(); 
            }
        }

        public static ObservableCollection<Personnels> userinfoList;

        public ObservableCollection<Personnels> UserinfoList
        {
            get 
            { 
                return userinfoList; 
            }
            set 
            { 
                userinfoList = value; 
                this.NotifyPropertyChanged(); 
            }
        }
        public bool isRunning;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public HomepageViewModel()
        {
            IsRunning = false;
            MyProfileViewModel.LoadTotal(PSACache.Instance.UserInfo.wpid);

            RefreshCommand = new Command<string>((key) =>
            {
                Views.Feeds.HomePage.LastIndex = 12;
                Views.Feeds.HomePage.isFirstLoad = false;
                MasterMenuViewModel.postbutton = string.Empty;
                homePostList.Clear();
                LoadData2();
                IsRefreshing = false;
            });

            homePostList = new ObservableCollection<Post>();
            LoadData2();

            this.InquireCommand = new Command(this.InquireClicked);
            this.Photo = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);

            userinfoList = new ObservableCollection<Personnels>();
            userinfoList.CollectionChanged += CollectionChanges;
        }

        /*public static void Insertimage(string url)
        {
            userinfoList.Add(new Personnels()
            {
                Avatar = url
            });
        }*/
        private async void CollectionChanges(object sender, EventArgs e)
        {
            await Task.Delay(100);
            this.Photo = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
        }

        /// <summary>
        /// Load post data in listview using observablecollection in HomePage
        /// </summary>
        public static void LoadData(string lastid)
        {
            try
            {
                SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, lastid, (bool success, string data) =>
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
                            string post_author = post.data[i].post_author; //user id
                            string id = post.data[i].id;
                            string content = post.data[i].content;
                            string category = post.data[i].item_category;
                            string title = post.data[i].title;
                            string date_post = post.data[i].date_post == string.Empty ? new DateTime().ToString() : post.data[i].date_post;
                            string type = post.data[i].type;
                            string item_image = post.data[i].item_image;
                            string author = post.data[i].author; // user avatar
                            string name = post.data[i].name; //dname
                            string views = post.data[i].views;
                            string post_link = post.data[i].post_link;
                            string vehicle_type = post.data[i].vehicle_date;
                            string pickup_location = post.data[i].pickup_location;
                            string do_price = "Drop-off: " + post.data[i].drop_off_location;
                            if (type == "Selling")
                            {
                                title = category + " : " + title;
                                content = post.data[i].content;
                                vehicle_type = "Vehicle: " + post.data[i].vehicle_date;
                                pickup_location = "Pick-up: " + post.data[i].pickup_location;
                                do_price = "Price: " + post.data[i].time_price;
                            }
                            if (type == "Pasabay")
                            {
                                //title = "Pasabuy - Hide";
                                title = "<b> Destination: " + post.data[i].pickup_location + " </b>";
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Return Place: " + post.data[i].drop_off_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }
                            if (type == "Pahatid")
                            {
                                //title = "Pasabuy - Hide";
                                title = "<b> Pick-up: " + post.data[i].pickup_location + " </b>";
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Drop-off: " + post.data[i].drop_off_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }
                            if (type == "Pabili")
                            {
                                title = "Item Name: " + post.data[i].title;
                                content = "Description: " + post.data[i].content;
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Location: " + post.data[i].pickup_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }

                            homePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link, post_author, pickup_location, vehicle_type, do_price));
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        public void LoadData2()
        {
            try
            {
                IsRunning = true;
                SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        IsRunning = false;
                        PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                        for (int i = 0; i < post.data.Length; i++)
                        {
                            string image_height = "-1";
                            if (post.data[i].item_image != "")
                            {
                                image_height = "400";
                            }
                            string post_author = post.data[i].post_author; //user id
                            string id = post.data[i].id;
                            string content = post.data[i].content;
                            string category = post.data[i].item_category;
                            string title = post.data[i].title;
                            string date_post = post.data[i].date_post == string.Empty ? new DateTime().ToString() : post.data[i].date_post;
                            string type = post.data[i].type;
                            string item_image = post.data[i].item_image;
                            string author = post.data[i].author; // user avatar
                            string name = post.data[i].name; //dname
                            string views = post.data[i].views;
                            string post_link = post.data[i].post_link;
                            string vehicle_type = post.data[i].vehicle_date;
                            string pickup_location = post.data[i].pickup_location;
                            string do_price = "Drop-off: " + post.data[i].drop_off_location;
                            if (type == "Selling")
                            {
                                title = category + " : " + title;
                                content = post.data[i].content;
                                vehicle_type = "Vehicle: " + post.data[i].vehicle_date;
                                pickup_location = "Pick-up: " + post.data[i].pickup_location;
                                do_price = "Price: " + post.data[i].time_price;
                            }
                            if (type == "Pasabay")
                            {
                                //title = "Pasabuy - Hide";
                                title = "<b> Destination: " + post.data[i].pickup_location + " </b>";
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Return Place: " + post.data[i].drop_off_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }
                            if (type == "Pahatid")
                            {
                                //title = "Pasabuy - Hide";
                                title = "<b> Pick-up: " + post.data[i].pickup_location + " </b>";
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Drop-off: " + post.data[i].drop_off_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }
                            if (type == "Pabili")
                            {
                                title = "Item Name: " + post.data[i].title;
                                content = "Description: " + post.data[i].content;
                                pickup_location = string.IsNullOrEmpty(post.data[i].vehicle_date) ? "Date: " : "Date: " + post.data[i].vehicle_date.Substring(0, 10);
                                vehicle_type = "Location: " + post.data[i].pickup_location;
                                do_price = "Time: " + post.data[i].time_price;
                            }

                            homePostList.Add(new Post(PSAProc.GetUrl(author),
                                name, type, date_post, views, title, content, PSAProc.GetUrl(item_image), image_height, id, post_link, post_author, pickup_location, vehicle_type, do_price));
                        }
                    }
                    else
                    {
                        IsRunning = false;
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception e)
            {
                IsRunning = false;
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        #endregion

        #region Property
        public ICommand RefreshCommand { protected set; get; }

        public ICommand InquireCommand { protected set; get; }

        private void InquireClicked(object obj)
        {
            //Get display name, user avatar(already fetched), and user id
            /*ChatMessageViewModel.ProfileNames = "test";
            ChatMessageViewModel.ProfileImages = this.Photo;
            ChatMessageViewModel.user_id = "3";
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ChatMessagePage()));*/
        }

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

        public string Placeholder
        {
            get
            {
                return "Write something here...";
            }
        }

        public string photo;
        public string Photo
        {
            get
            {
                return photo;
            }
            set
            {
                if (photo != value)
                {
                    photo = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Method
        #endregion
    }
}
