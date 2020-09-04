using PasaBuy.App.Models.Feeds;
using System;
using System.Collections.Generic;
using PasaBuy.App.Controllers;
using System.Collections.ObjectModel;
using System.Text;
using PasaBuy.App.Controllers.Notice;
using Newtonsoft.Json;

namespace PasaBuy.App.ViewModels.Feeds
{
    public class HomepageViewModel
    {
        public HomepageViewModel()
        {
            //This is a Test.
            homePostList = new ObservableCollection<Post>();
            Random rnd = new Random();

            SocioPress.Feeds.Instance.Home(UserPrefs.Instance.UserInfo.wpid, UserPrefs.Instance.UserInfo.snky, "", (bool success, string data) =>
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

                            "https://scontent.fmnl4-3.fna.fbcdn.net/v/t1.0-9/106719775_4086437514762774_502557937090430002_n.jpg?_nc_cat=110&_nc_sid=85a577&_nc_oc=AQkcZ-BY-AieNPu8vCPiM95uBIF_0eDQJeZpcZyiM32uFg8ZBxXKq0fer6WGvCYJbIw&_nc_ht=scontent.fmnl4-3.fna&oh=0abe1bc6e34bdbb2b06756427fe79021&oe=5F2C4EEF",
                            name,
                            type,
                            date_post,
                            rnd.Next(500, 10000).ToString(),
                            title,
                            content,
                            "https://scontent.fmnl4-5.fna.fbcdn.net/v/t1.0-9/106969425_3015120108557584_6246739416417392559_n.jpg?_nc_cat=106&_nc_sid=3b2858&_nc_oc=AQmDLpNqBugjnzD87aO5sr5wyCfIeyuTEImTK8P1JWHrED6p4zks83b7SKcXyHgGXdw&_nc_ht=scontent.fmnl4-5.fna&oh=10bbcb6208fe4ffe107349d204e6a882&oe=5F2CCF3B"
                            ));
                    }
                }
                else
                {
                    new Alert("Notice to User", HtmlUtilities.ConvertToPlainText(data), "Try Again");

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
                return UserPrefs.Instance.UserInfo.avatarUrl;
            }
        }

        public ObservableCollection<Post> homePostList;

        public ObservableCollection<Post> HomePosts
        {
            get { return homePostList; }
            set { this.homePostList = value; }
        }
    }
}
