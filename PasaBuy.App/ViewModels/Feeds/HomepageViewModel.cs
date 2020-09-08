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
            homePostList = new ObservableCollection<Post>();
            Random rnd = new Random();


            SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
            {
                if (success)
                {
                    PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                    for (int i = 0; i < post.data.Length; i++)
                    {
                        string id = post.data[i].id;
                        string name = post.data[i].name;
                        string status = post.data[i].status;
                        string title = post.data[i].title;
                        string content = post.data[i].content;
                        string date_post = post.data[i].date_post;
                        string type = post.data[i].type;
                        string item_name = post.data[i].item_name;
                        string pickup_location = post.data[i].pickup_location;
                        string vehicle_type = post.data[i].vehicle_type;
                        string drop_off_location = post.data[i].drop_off_location;
                        string author = post.data[i].author;
                        string views = post.data[i].views;

                        homePostList.Add(new Post(
                                        "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/ArticleImage2.png",
                            name,
                            type,
                            date_post,
                            rnd.Next(500, 10000).ToString(),
                                        title,
                                        content,
                                        "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/ArticleImage2.png"
                                    ));
                    }
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                }
            });
        }
        public static void RefreshList()
        {
            Random rnd = new Random();

            homePostList.Clear();
            SocioPress.Feeds.Instance.Home(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", (bool success, string data) =>
            {
                if (success)
                {
                    PostListData post = JsonConvert.DeserializeObject<PostListData>(data);
                    for (int i = 0; i < post.data.Length; i++)
                    {
                        string name = post.data[i].name;
                        string type = post.data[i].type;
                        string status = post.data[i].status;
                        string title = post.data[i].title;
                        string content = post.data[i].content;
                        string date_post = post.data[i].date_post;

                        homePostList.Add(new Post(
                                        "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/ArticleImage2.png",
                            name,
                            type,
                            date_post,
                            rnd.Next(500, 10000).ToString(),
                                        title,
                                        content,
                                        "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/ArticleImage2.png"
                                    ));
                    }
                }
                else
                {
                    new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                }
            });
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
                return PSACache.Instance.UserInfo.avatarUrl;
            }
        }
    }
}
