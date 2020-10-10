using System;
using System.Collections.ObjectModel;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.Views.Master;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Syncfusion.ListView.XForms;
using PasaBuy.App.Behaviors.Chat;
using USocketNet;
using SocioPress;

namespace PasaBuy.App.ViewModels.Chat
{
    /// <summary>
    /// ViewModel for chat message page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatMessageViewModel : BaseViewModel
    {
        /// <summary>
        /// Declared all variables needed. 
        /// </summary>
        #region Fields

        /// <summary>
        /// ids for index of loadmore.
        /// </summary>
        public int ids = 0;

        /// <summary>
        /// For double clicked in send message clicked.
        /// </summary>
        public int count = 0;

        //public static int refresh = 0;

        /// <summary>
        /// Stored the store id. 
        /// </summary>
        public static string storeid = "0";

        /// <summary>
        /// Type for user, move and store.
        /// </summary>
        public static string type = "0";

        /// <summary>
        /// For loading indicator.
        /// </summary>
        bool _isBusy = false;

        /// <summary>
        /// For loadmore.
        /// </summary>
        bool _isLoad = false;

        /// <summary>
        /// For checking of first id in list.
        /// </summary>
        public static bool isFirstID = false;

        /// <summary>
        /// Variable of text input new message.
        /// </summary>
        private string newMessage;

        /// <summary>
        /// Check if from message setting or profile page.
        /// </summary>
        public static string myPage = string.Empty;

        /// <summary>
        /// WPID of user who want to chat.
        /// </summary>
        public static string user_id = string.Empty;

        /// <summary>
        /// Static to get the DisplayName of user.
        /// </summary>
        public static string ProfileNames = string.Empty;

        /// <summary>
        /// Stored the DisplayName of user then display to ProfileName.
        /// </summary>
        private string profileName = ProfileNames;

        /// <summary>
        /// Static to get the avatar of user.
        /// </summary>
        public static string ProfileImages = string.Empty;

        /// <summary>
        /// Stored the avatar of user then display to ProfileImage.
        /// </summary>
        private string profileImage = ProfileImages;

        /// <summary>
        /// For observable collection list.
        /// </summary>
        ChatList _chatHistoryList = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageViewModel" /> class.
        /// </summary>
        public ChatMessageViewModel()
        {
            List<ChatListDetails> list = new List<ChatListDetails>();
            ChatList = new ChatList(list);
            FirstLoad();
            isFirstID = false;
            ids = 0;
            count = 0;

            this.MakeVoiceCall = new Command(this.VoiceCallClicked);
            this.MakeVideoCall = new Command(this.VideoCallClicked);
            this.MenuCommand = new Command(this.MenuClicked);
            this.ShowCamera = new Command(this.CameraClicked);
            this.SendAttachment = new Command(this.AttachmentClicked);
            this.SendCommand = new Command(this.SendClicked);
            this.BackCommand = new Command(this.BackButtonClicked);
            this.ProfileCommand = new Command(this.ProfileClicked);
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
            USNMessage.Instance.OnMessage = OnMessage;
        }


        /// <summary>
        /// Pop-up message for send and recieve message.
        /// </summary>
        private void OnMessage(USocketNet.Model.Message msg)
        {
            if (msg.s == PSACache.Instance.UserInfo.wpid)
            {
                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), msg.m, false));
            }

            if (msg.s == user_id)
            {
                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), msg.m, true));
            }
        }

        /// <summary>
        /// FirstLoad for Loadmore.
        /// </summary>
        public async void FirstLoad()
        {
            ChatMessageListViewBehavior.isFirstLoad = false;
            await Task.Delay(100);
            LoadMessage(user_id, "", "");

            /*Device.StartTimer(TimeSpan.FromSeconds(1), doitt);
            bool doitt()
            {
                if (refresh == 0)
                {
                    if (myPage != "profile")
                    {
                        RecentChatViewModel.chatItems.Clear();
                        RecentChatViewModel.LoadMesssage("");
                    }
                    PopupMessagePopupMessage();
                    return true;
                }
                else
                {
                    return false;
                }
            }*/

        }


        /// <summary>
        /// User for can load more or not.
        /// </summary>
        private bool CanLoadMoreItems(object obj)
        {
            return isLoad;
        }


        /// <summary>
        /// Load 12 Message then used for load more.
        /// </summary>
        public void LoadMessage(string recipient, string offset, string lastid)
        {
            try
            {
                SocioPress.Message.Instance.GetByRecepient(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, recipient, offset, type, storeid, lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ChatData chat = JsonConvert.DeserializeObject<ChatData>(data);
                        if (lastid == "")
                        {
                            int len = offset != string.Empty ? 7 : 12;
                            isLoad = chat.data.Length < len ? false : true;
                        }
                        for (int i = 0; i < chat.data.Length; i++)
                        {
                            string id = chat.data[i].id;
                            string senders = chat.data[i].sender;
                            string content = chat.data[i].content;
                            string date_created = chat.data[i].date_created;
                            bool isreceived = senders != PSACache.Instance.UserInfo.wpid ? true : false;
                            CultureInfo provider = new CultureInfo("fr-FR");
                            DateTime datenow = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime datedb = DateTime.ParseExact(date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            TimeSpan ts = datedb - datenow;
                            var currentTime = DateTime.Now;

                            if (lastid == "")
                            {
                                ChatList.Insert(0, new ChatListItem(id, "", currentTime.AddMinutes(ts.TotalMinutes), content, isreceived));
                            }
                            else
                            {
                                ChatList.Add(new ChatListItem(id, "", currentTime.AddMinutes(ts.TotalMinutes), content, isreceived));
                            }
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

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the ChatList Observable Collection.
        /// </summary>
        public ChatList ChatList
        {
            get => _chatHistoryList;
            set
            {
                _chatHistoryList = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the isLoad.
        /// </summary>
        public bool isLoad
        {
            get
            {
                return _isLoad;
            }
            set
            {
                if (_isLoad != value)
                {
                    _isLoad = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the isBusy.
        /// </summary>
        public bool isBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string ProfileName
        {
            get
            {
                return this.profileName;
            }

            set
            {
                this.profileName = value;
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

        /// <summary>
        /// Gets or sets a new message.
        /// </summary>
        public string NewMessage
        {
            get
            {
                return this.newMessage;
            }

            set
            {
                this.newMessage = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the LoadMoreItemsCommand is clicked.
        /// </summary>
        public Command<object> LoadMoreItemsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the voice call button is clicked.
        /// </summary>
        public Command MakeVoiceCall { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the video call button is clicked.
        /// </summary>
        public Command MakeVideoCall { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the menu button is clicked.
        /// </summary>
        public Command MenuCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the camera button is clicked.
        /// </summary>
        public Command ShowCamera { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the attachment button is clicked.
        /// </summary>
        public Command SendAttachment { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the send button is clicked.
        /// </summary>
        public Command SendCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the back button is clicked.
        /// </summary>
        public Command BackCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the voice call button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VoiceCallClicked(object obj)
        {
            new Alert("Demoguy Notice", "Voice call is not yet implemented, stay tune on our advisory section for updates. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the video call button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VideoCallClicked(object obj)
        {
            new Alert("Demoguy Notice", "Video call is not yet implemented, stay tune on our advisory section for updates. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuClicked(object obj)
        {
            new Alert("Demoguy Notice", "We plan to add a popup window as an action list for this current conversation. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the camera icon is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void CameraClicked(object obj)
        {
            new Alert("Demoguy Notice", "Sorry! Camera or Gallery upload is not yet implemented. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the attachment icon is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void AttachmentClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the send button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void SendClicked(object obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.NewMessage))
                {
                    if (count == 0)
                    {
                        ChatMessageListViewBehavior.isFirstLoad = false;
                        string types = type == "2" ? "4" : type;
                        count = 1;
                        SocioPress.Message.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, this.NewMessage, user_id, types, storeid, (bool success, string data) =>
                        {
                            if (success)
                            {
                                USNMessage.Instance.SendMessage(user_id, this.NewMessage, null);
                                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), this.NewMessage, false));
                                this.NewMessage = null;
                                count = 0;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                                count = 0;
                            }
                        });
                    }
                }
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        /* public void PopupMessage()
         {
             if (ChatList.Count != 0)
             {
                 LoadMessage(user_id, "", ChatList.Last().ID);
             }
             else
             {
                 LoadMessage(user_id, "", "");
             }
         }*/

        /// <summary>
        /// Invoked when the back button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void BackButtonClicked(object obj)
        {
            //App.Current.MainPage = new NavigationPage(new RecentChatPage());
        }

        /// <summary>
        /// Invoked when the Profile name is clicked.
        /// </summary>
        private void ProfileClicked(object obj)
        {
            // Do something
        }


        /// <summary>
        /// Invoked when the LoadMore Button is clicked.
        /// </summary>
        private async void LoadMoreItems(object obj)
        {
            try
            {
                ChatMessageListViewBehavior.isFirstLoad = true;
                isBusy = true;
                await Task.Delay(1000);
                if (isFirstID)
                {
                    ids += 7;
                }
                else
                {
                    isFirstID = true;
                }
                LoadMessage(user_id, ids.ToString(), "");
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        #endregion
    }
}
