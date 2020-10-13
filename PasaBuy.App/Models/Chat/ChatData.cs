namespace PasaBuy.App.Models.Chat
{
    public class ChatData
    {
        public ChatDetails[] data;
        public class ChatDetails
        {
            public string id = string.Empty;
            public string sender = string.Empty;
            public string content = string.Empty;
            public string date_seen = string.Empty;
            public string date_created = string.Empty;
        }
    }
}
