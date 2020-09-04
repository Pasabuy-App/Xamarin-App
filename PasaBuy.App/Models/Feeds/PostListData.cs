using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Feeds
{
    public class PostListData
    {

        public PostData[] data;
        public class PostData
        {
            public string id = string.Empty;
            public string name = string.Empty;
            public string status = string.Empty;
            public string title = string.Empty;
            public string content = string.Empty;
            public string date_post = string.Empty;
            public string type = string.Empty;
        }
        private string id;
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        private string date_post;
        public string Date_post
        {
            get { return date_post; }
            set { date_post = value; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
