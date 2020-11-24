using Newtonsoft.Json;
using PasaBuy.App.Behaviors.Chat;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Driver;
using PasaBuy.App.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Driver
{
    /// <summary>
    /// ViewModel for chat message page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DriverChatMessageViewModel : BaseViewModel
    {
        #region Fields
        public int count = 0;
        public static int refresh = 0;
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
        public static string myPage = string.Empty;

        public static string odid = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageViewModel" /> class.
        /// </summary>
        public DriverChatMessageViewModel()
        {
            List<ChatListDetails> list = new List<ChatListDetails>();
            ChatList = new ChatList(list);
            FirstLoad();
            isFirstID = false;
            ChatMessageListViewBehavior.isFirstLoad = false;
            ids = 0;

            this.MakeVoiceCall = new Command(this.VoiceCallClicked);
            this.MakeVideoCall = new Command(this.VideoCallClicked);
            this.MenuCommand = new Command(this.MenuClicked);
            this.ShowCamera = new Command(this.CameraClicked);
            this.SendAttachment = new Command(this.AttachmentClicked);
            this.SendCommand = new Command(this.SendClicked);
            this.BackCommand = new Command(this.BackButtonClicked);
            this.ProfileCommand = new Command(this.ProfileClicked);
            LoadMoreItemsCommand = new Command<object>(LoadMoreItems, CanLoadMoreItems);
            USocketNet.USNMessage.Instance.OnMessage = OnMessage;
        }
        private void OnMessage(USocketNet.Model.Message msg)
        {
            if (msg.s == PSACache.Instance.UserInfo.mvid)
            {
                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), msg.m, false));
            }

            if (msg.s == user_id)
            {
                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), msg.m, true));
            }
        }
        public async void FirstLoad()
        {
            ChatMessageListViewBehavior.isFirstLoad = false;
            await Task.Delay(100);
            LoadMessage("");
        }

        private bool CanLoadMoreItems(object obj)
        {
            return isLoad;
        }

        public void LoadMessage(string offset)
        {
            try
            {
                Http.SocioPress.Message.Instance.GetByRecepient(user_id, PSACache.Instance.UserInfo.mvid, offset, "mover", odid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ChatData chat = JsonConvert.DeserializeObject<ChatData>(data);
                        isLoad = chat.data.Length < 7 ? false : true;
                        for (int i = 0; i < chat.data.Length; i++)
                        {
                            bool isreceived = chat.data[i].sender != PSACache.Instance.UserInfo.mvid ? true : false;
                            CultureInfo provider = new CultureInfo("fr-FR");
                            DateTime datenow = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime datedb = DateTime.ParseExact(chat.data[i].date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            TimeSpan ts = datedb - datenow;
                            var currentTime = DateTime.Now;

                            ChatList.Insert(0, new ChatListItem(chat.data[i].id, "", currentTime.AddMinutes(ts.TotalMinutes), chat.data[i].content, isreceived));

                        }
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: SPV1MSG-GBR1DCMVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1MSG-GBR1DCMVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1MSG-GBR1DCMVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1MSG-GBR1DCMVM-" + err.ToString());
                }
            }
        }

        #endregion

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
            new Controllers.Notice.Alert("Demoguy Notice", "Voice call is not yet implemented, stay tune on our advisory section for updates. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the video call button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void VideoCallClicked(object obj)
        {
            new Controllers.Notice.Alert("Demoguy Notice", "Video call is not yet implemented, stay tune on our advisory section for updates. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void MenuClicked(object obj)
        {
            new Controllers.Notice.Alert("Demoguy Notice", "We plan to add a popup window as an action list for this current conversation. Thank you for your patience!", "AGREE");
        }

        /// <summary>
        /// Invoked when the camera icon is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void CameraClicked(object obj)
        {
            new Controllers.Notice.Alert("Demoguy Notice", "Sorry! Camera or Gallery upload is not yet implemented. Thank you for your patience!", "AGREE");
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
                /*this.ChatMessageInfo.Add(new ChatMessage
                {
                    Message = this.NewMessage,
                    Time = DateTime.Now
                });*/

                ChatMessageListViewBehavior.isFirstLoad = false;
                try
                {
                    if (count == 0)
                    {
                        count = 1;
                        await Task.Delay(200);
                        Http.SocioPress.Message.Instance.Insert(this.NewMessage, user_id, PSACache.Instance.UserInfo.mvid, "mover", odid, (bool success, string data) =>
                        {
                            if (success)
                            {
                                if (USocketNet.USNMessage.Instance.IsConnected)
                                {
                                    USocketNet.USNMessage.Instance.SendMessage(user_id, this.NewMessage, null);
                                }
                                ChatList.Add(new ChatListItem("0", "", DateTime.Now.AddMinutes(0), this.NewMessage, false));
                                this.NewMessage = null;
                                count = 0;
                            }
                            else
                            {
                                new Controllers.Notice.Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            }
                        });
                        count = 0;
                    }
                }
                catch (Exception err)
                {
                    if (PSAConfig.isDebuggable)
                    {
                        new Controllers.Notice.Alert("Error Code: SPV1MSG-I1DCMVM", err.ToString(), "OK");
                        Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-SPV1MSG-I1DCMVM-" + err.ToString());
                    }
                    else
                    {
                        new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1MSG-I1DCMVM.", "OK");
                        Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-SPV1MSG-I1DCMVM-" + err.ToString());
                    }
                }
            }

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
                await Task.Delay(200);
                if (isFirstID)
                {
                    ids += 7;
                }
                else
                {
                    ids = 12;
                    isFirstID = true;
                }
                LoadMessage(ids.ToString());
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: SPV1MSG-GBR1DCMVM.", "OK");
            }
            finally
            {
                isBusy = false;
            }
        }

        #endregion
    }
}
