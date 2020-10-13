using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Chat;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class MessagesViewModel : BaseViewModel
    {
        public static ObservableCollection<ChatDetail> storeChatList;

        public ObservableCollection<ChatDetail> StoreChatList
        {
            get { return storeChatList; }
            set { storeChatList = value; this.NotifyPropertyChanged(); }
        }
        public MessagesViewModel()
        {
            storeChatList = new ObservableCollection<ChatDetail>();
            storeChatList.Clear();
            LoadMesssage("");
        }
        public static void LoadMesssage(string offset)
        {
            try
            {
                SocioPress.Message.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "2", PSACache.Instance.UserInfo.stid, offset, (bool success, string data) =>
                {
                    if (success)
                    {
                        ChatDataList chat = JsonConvert.DeserializeObject<ChatDataList>(data);
                        for (int i = 0; i < chat.data.Length; i++)
                        {
                            string id = chat.data[i].ID;
                            string user_id = chat.data[i].user_id;
                            string store_id = chat.data[i].store_id;
                            string types = chat.data[i].types;
                            string name = chat.data[i].name;
                            string content = chat.data[i].content;
                            string date_created = chat.data[i].date_created == null ? "" : chat.data[i].date_created;
                            string date_seen = chat.data[i].date_seen == null ? "" : chat.data[i].date_seen;
                            string avatar = chat.data[i].avatar;
                            string sender_id = chat.data[i].sender_id;

                            string notitype = sender_id == PSACache.Instance.UserInfo.wpid ? notitype = date_seen == "" ? "Sent" : "Received" : date_seen == "" ? "New" : "Viewed";

                            string showdate = string.Empty;
                            CultureInfo provider = new CultureInfo("fr-FR");
                            DateTime datetoday = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime mydate = DateTime.ParseExact(date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            TimeSpan ts = datetoday - mydate;
                            if (datetoday.Day == mydate.Day)
                            {
                                if (ts.TotalMinutes < 60)
                                {
                                    showdate = ts.TotalMinutes < 2 ? ts.Minutes == 0 ? "Now" : "1 min ago" : ts.Minutes + " mins ago";
                                }
                                else
                                {
                                    showdate = ts.Hours > 1 ? ts.Hours + " hrs ago" : ts.Hours + " hr ago";
                                }
                            }
                            else
                            {
                                if (ts.Days == 1)
                                {
                                    showdate = "Yesterday";
                                }
                                else
                                {
                                    int mos = datetoday.Month - mydate.Month;
                                    showdate = mos < 2 ? mydate.ToString("MMM dd") : mydate.ToString("MM/dd/yyyy");
                                }
                            }

                            storeChatList.Add(new ChatDetail(user_id, PSAProc.GetUrl(avatar), name, showdate, content, "Text", notitype, "", store_id, types));
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
    }
}
