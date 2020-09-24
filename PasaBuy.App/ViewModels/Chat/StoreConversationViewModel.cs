using FFImageLoading;
using Newtonsoft.Json;
using PasaBuy.App.Behaviors.Chat;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Chat
{
    [Preserve(AllMembers = true)]
    public class StoreConversationViewModel : BaseViewModel
    {
        #region Fields
        public int count = 0;
        public static int refresh = 0;

        public static string storeid = "0";
        public static string type = "0";
        bool _isBusy = false;
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
        bool _isLoad = false;
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

        private string newMessage;

        public static string user_id = string.Empty;
        public static string ProfileNames = string.Empty;
        private string profileName = ProfileNames;
        public static string ProfileImages = string.Empty;
        private string profileImage = ProfileImages;
        public Command<object> LoadMoreItemsCommand { get; set; }

        ChatList _chatHistoryList = null;
        public ChatList ChatList
        {
            get => _chatHistoryList;
            set
            {
                _chatHistoryList = value;
                this.NotifyPropertyChanged();
            }
        }

        public static bool isFirstID = false;
        public int ids = 0;
        #endregion
        public StoreConversationViewModel()
        {
            this.MakeVoiceCall = new Command(this.VoiceCallClicked);
            this.MakeVideoCall = new Command(this.VideoCallClicked);
            this.MenuCommand = new Command(this.MenuClicked);
            this.ShowCamera = new Command(this.CameraClicked);
            this.SendAttachment = new Command(this.AttachmentClicked);
            this.SendCommand = new Command(this.SendClicked);
            this.BackCommand = new Command(this.BackButtonClicked);
            this.ProfileCommand = new Command(this.ProfileClicked);
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);

            List<ChatListDetails> list = new List<ChatListDetails>();
            ChatList = new ChatList(list);
            FirstLoad();
            isFirstID = false;
            ids = 0;
            ChatMessageListViewBehavior.isFirstLoad = false;
            //this.GenerateMessageInfo();
        }
        public async void FirstLoad()
        {
            ChatMessageListViewBehavior.isFirstLoad = false;
            await Task.Delay(100);
            LoadMessage(user_id, "", "");

            Device.StartTimer(TimeSpan.FromSeconds(1), doitt);
            bool doitt()
            {
                if (refresh == 0)
                {
                    StoreMessageViewModel.storeChatList.Clear();
                    StoreMessageViewModel.LoadMesssage("");
                    PopupMessage();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool CanLoadMoreItems(object obj)
        {
            return isLoad;
        }

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
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20475.", "OK");
            }
        }

        #region Public Properties

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
        private async void SendClicked(object obj)
        {
            if (!string.IsNullOrWhiteSpace(this.NewMessage))
            {

                ChatMessageListViewBehavior.isFirstLoad = false;
                try
                {
                    string types = "0";
                    if (type == "1")
                    {
                        types = "2";
                    }
                    else
                    {
                        types = type;
                    }
                    if (count == 0)
                    {
                        count = 1;
                        await Task.Delay(200);
                        SocioPress.Message.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, this.NewMessage, user_id, "2", storeid, (bool success, string data) =>
                        {
                            if (success)
                            {
                                this.NewMessage = null;
                            }
                            else
                            {
                                new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                        count = 0;
                    }
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator. Error Code: 20476.", "OK");
                }
            }

        }
        public void PopupMessage()
        {
            //await Task.Delay(500);
            LoadMessage(user_id, "", ChatList.Last().ID);
        }

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
            catch
            {

            }
            finally
            {
                isBusy = false;
            }
        }

        #endregion
    }
}
