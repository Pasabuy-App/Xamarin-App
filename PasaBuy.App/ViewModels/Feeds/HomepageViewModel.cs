using PasaBuy.App.Models.Feeds;
using System;
using System.Collections.Generic;
using PasaBuy.App.Controllers;
using System.Collections.ObjectModel;
using System.Text;
using PasaBuy.App.Controllers.Notice;
using Newtonsoft.Json;
using PasaBuy.App.Local;
using System.Windows.Input;
using Xamarin.Forms;
using PasaBuy.App.Models.Onboarding;

namespace PasaBuy.App.ViewModels.Feeds
{
    public class HomepageViewModel : BaseViewModel
    {

        public static ObservableCollection<Post> homePostList;

        public ObservableCollection<Post> HomePosts
        {
            get { return homePostList; }
            set { homePostList = value; this.NotifyPropertyChanged(); }
        }
        public HomepageViewModel()
        {
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                homePostList = new ObservableCollection<Post>();
                SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
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
                LoadTotal();
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
                SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
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
                        MyProfileViewModel.postcount = getdata.data.count;
                    }
                });

                SocioPress.Reviews.Instance.Get(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        MyProfileViewModel.ratingscount = getdata.data.ave_rating;
                    }
                });
                SocioPress.Transaction.Instance.GetTotal(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    if (success)
                    {
                        ProfileGetData getdata = JsonConvert.DeserializeObject<ProfileGetData>(data);
                        MyProfileViewModel.transactcount = getdata.data.transac;
                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
            }
        }

        public string Placeholder
        {
            get
            {
                return "Write something here...";
            }
        }

        public string Photo
        {
            get
            {
                return PSAProc.GetUrl(PSACache.Instance.UserInfo.avatarUrl);
            }
        }
    }
}
