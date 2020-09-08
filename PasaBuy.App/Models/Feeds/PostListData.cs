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
            public string item_name = string.Empty;
            public string pickup_location = string.Empty;
            public string vehicle_type = string.Empty;
            public string drop_off_location = string.Empty;
            public string author = string.Empty;
            public string views = string.Empty;
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
        private string item_name;
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        private string pickup_location;
        public string Pickup_location
        {
            get { return pickup_location; }
            set { pickup_location = value; }
        }
        private string vehicle_type;
        public string Vehicle_type
        {
            get { return vehicle_type; }
            set { vehicle_type = value; }
        }
        private string drop_off_location;
        public string Drop_off_location
        {
            get { return drop_off_location; }
            set { drop_off_location = value; }
        }
        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string views;
        public string Views
        {
            get { return views; }
            set { views = value; }
        }
    }
}
