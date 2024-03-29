﻿namespace PasaBuy.App.Models.Feeds
{
    public class PostListData
    {

        public PostData[] data;
        public class PostData
        {
            public string id = string.Empty;
            public string name = string.Empty;
            public string post_author = string.Empty;
            public string post_link = string.Empty;
            public string title = string.Empty;
            public string content = string.Empty;
            public string date_post = string.Empty;
            public string status = string.Empty;
            public string type = string.Empty;
            public string item_category = string.Empty;
            public string pickup_location = string.Empty;
            public string vehicle_date = string.Empty;
            public string drop_off_location = string.Empty;
            public string item_image = string.Empty;
            public string author = string.Empty;
            public string views = string.Empty;
            public string time_price = string.Empty;
        }
    }
}
