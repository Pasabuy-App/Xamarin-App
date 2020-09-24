using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Chat
{
    public class ChatDataList
    {
        public ChatList[] data;
        public class ChatList
        {
            public string ID = string.Empty;
            public string store_name = string.Empty;
            public string store_avatar = string.Empty;
            public string user_id = string.Empty;
            public string store_id = string.Empty;
            public string types = string.Empty;
            public string name = string.Empty;
            public string content = string.Empty;
            public string date_seen = string.Empty;
            public string date_created = string.Empty;
            public string avatar = string.Empty;
            public string sender_id = string.Empty;
        }
    }
}
