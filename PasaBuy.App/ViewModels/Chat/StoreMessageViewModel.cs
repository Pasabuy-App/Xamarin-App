using FFImageLoading;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.ViewModels.Driver;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Chat
{
    /// <summary>
    /// View model for store chat page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class StoreMessageViewModel : BaseViewModel
    {

        #region Fields

        private string profileImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);

        public static ObservableCollection<ChatDetail> storeChatList;

        #endregion

        #region Constuctor
        public StoreMessageViewModel()
        {
            storeChatList = new ObservableCollection<ChatDetail>();
            storeChatList.Clear();
            LoadMesssage("");

            this.MakeVoiceCallCommand = new Command(this.VoiceCallClicked);
            this.MakeVideoCallCommand = new Command(this.VideoCallClicked);
            this.ShowSettingsCommand = new Command(this.SettingsClicked);
            this.MenuCommand = new Command(this.MenuClicked);
            this.ProfileImageCommand = new Command(this.ProfileImageClicked);
        }
        public static void LoadMesssage(string offset)
        {
            try
            {
                if (MasterView.MyType == "store")
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
                if (MasterView.MyType == "mover")
                {
                    SocioPress.Message.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "1", "0", offset, (bool success, string data) =>
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
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the ChatList ObservableCollection.
        /// </summary>
        public ObservableCollection<ChatDetail> ChatList
        {
            get
            {
                return storeChatList;
            }
            set
            {
                storeChatList = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ProfileImage
        {
            get
            {
                return this.profileImage;
            }

            set
            {
                this.profileImage = value;
                this.NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Declration of itemSelectedCommand.
        /// </summary>
        private Command itemSelectedCommand;

        /// <summary>
        /// Gets or sets the command that is executed when the voice call button is clicked.
        /// </summary>
        public Command MakeVoiceCallCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the video call button is clicked.
        /// </summary>
        public Command MakeVideoCallCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the settings button is clicked.
        /// </summary>
        public Command ShowSettingsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        /// Gets the command that is executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        /// <summary>
        /// Gets or sets the command that is executed when the profile image is clicked.
        /// </summary>
        public Command ProfileImageCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        private void ItemSelected(object selectedItem)
        {
            if (MasterView.MyType == "store")
            {
                StoreConversationViewModel.refresh = 0;
                StoreConversationViewModel.ProfileNames = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).SenderName;
                StoreConversationViewModel.ProfileImages = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ImagePath;
                StoreConversationViewModel.user_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ID;
                StoreConversationViewModel.storeid = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).Store_id;
                StoreConversationViewModel.type = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).Types;
                ((App.Current.MainPage as MasterDetailPage).Detail as NavigationPage).Navigation.PushModalAsync(new StoreConversationPage());
            }
            if (MasterView.MyType == "mover")
            {
                DriverChatMessageViewModel.refresh = 0;
                DriverChatMessageViewModel.ProfileNames = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).SenderName;
                DriverChatMessageViewModel.ProfileImages = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ImagePath;
                DriverChatMessageViewModel.user_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ID;
                DriverChatMessageViewModel.type = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).Types;
                ((App.Current.MainPage as MasterDetailPage).Detail as NavigationPage).Navigation.PushModalAsync(new DriverChatMessagePage());
            }
        }

        /// <summary>
        /// Invoked when the Profile image is clicked.
        /// </summary>
        private void ProfileImageClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the voice call button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VoiceCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the video call button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VideoCallClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the settings button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SettingsClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void MenuClicked(object obj)
        {
            // Do something
        }

        #endregion

    }
}