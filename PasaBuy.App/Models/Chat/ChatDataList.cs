namespace PasaBuy.App.Models.Chat
{
    public class ChatDataList
    {
        public ChatList[] data;
        public class ChatList
        {
            public string ID = string.Empty;
            public string content = string.Empty;
            public string sender = string.Empty;
            public string recipient = string.Empty;
            public string user_id = string.Empty;
            public string type = string.Empty;
            public string status = string.Empty;
            public string created_by = string.Empty;
            public string date_seen = string.Empty;
            public string date_created = string.Empty;
            public string avatar = string.Empty;
            public string name = string.Empty;
        }
    }
}