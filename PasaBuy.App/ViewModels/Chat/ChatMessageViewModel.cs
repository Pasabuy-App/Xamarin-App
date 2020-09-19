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

namespace PasaBuy.App.ViewModels.Chat
{
    /// <summary>
    /// ViewModel for chat message page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatMessageViewModel : BaseViewModel
    {
        #region Fields
        /// <summary>
        /// Stores the message text in an array. 
        /// </summary>
        private readonly string[] descriptions = { "Hi, can you tell me the specifications of the Dell Inspiron 5370 laptop?",
            " * Processor: Intel Core i5-8250U processor " +
            "\n" + " * OS: Pre-loaded Windows 10 with lifetime validity" +
            "\n" + " * Display: 13.3-inch FHD (1920 x 1080) LED display" +
            "\n" + " * Memory: 8GB DDR RAM with Intel UHD 620 Graphics" +
            "\n" + " * Battery: Lithium battery",
            "How much battery life does it have with wifi and without?",
            "Approximately 5 hours with wifi. About 7 hours without.",
        };

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

        //private string profileImage = PSAConfig.sfRootUrl + "ProfileImage3.png";

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

        public static bool isFirstLoad = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageViewModel" /> class.
        /// </summary>
        public ChatMessageViewModel()
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

            //this.GenerateMessageInfo();
        }
        public async void FirstLoad()
        {
            isFirstLoad = false;
            await Task.Delay(100);
            LoadMessage(user_id, "");
        }

        private bool CanLoadMoreItems(object obj)
        {
            return isLoad; 
        }

        public void LoadMessage(string sender, string lastid)
        {
            try
            {
                SocioPress.Message.Instance.GetByRecepient(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, sender, lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        ChatData chat = JsonConvert.DeserializeObject<ChatData>(data);
                        //Console.WriteLine("length: "+ chat.data.Length);
                        int len = 12;
                        if (lastid != string.Empty) { len = 7; }
                        if (chat.data.Length < len)
                        {
                            isLoad = false;
                        }
                        else
                        {
                            isLoad = true;
                        }
                        for (int i = 0; i < chat.data.Length; i++)
                        {
                            bool isreceived = false;
                            string id = chat.data[i].id;
                            string senders = chat.data[i].sender;
                            string content = chat.data[i].content;
                            string date_created = chat.data[i].date_created;
                            if (senders != PSACache.Instance.UserInfo.wpid)
                            {
                                isreceived = true;
                            }

                            CultureInfo provider = new CultureInfo("fr-FR");
                            DateTime datenow = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", provider);
                            DateTime datedb = DateTime.ParseExact(date_created, "yyyy-MM-dd HH:mm:ss", provider);
                            TimeSpan ts = datedb - datenow;
                            var currentTime = DateTime.Now;

                            //ChatList.Add(new ChatListItem(id, "", currentTime.AddMinutes(ts.TotalMinutes), content, isreceived));
                            ChatList.Insert(0, new ChatListItem(id, "", currentTime.AddMinutes(ts.TotalMinutes), content, isreceived));
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
                new Alert("Something went Wrong", "Please contact administrator.", "OK");
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
        /// Gets or sets a collection of chat messages.
        /// </summary>
        /*public ObservableCollection<ChatMessage> ChatMessageInfo
        {
            get
            {
                return this.chatMessageInfo;
            }

            set
            {
                this.chatMessageInfo = value;
                this.NotifyPropertyChanged();
            }
        }*/

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
        /// Initializes a collection and add it to the message items.
        /// </summary>
        private void GenerateMessageInfo()
        {
            /*var currentTime = DateTime.Now;
            chatMessageInfo.Add(new ChatMessage()
            {
                Message = this.descriptions[0],
                Time = currentTime.AddMinutes(-2517),
                IsReceived = true
            });
            chatMessageInfo.Add(new ChatMessage()
            {
                Message = this.descriptions[1],
                Time = currentTime.AddMinutes(-2408),
                IsReceived = false
            });
            chatMessageInfo.Add(new ChatMessage()
            {
                ImagePath = PSAConfig.sfRootUrl + "Electronics.png",
                Time = currentTime.AddMinutes(-2405),
                IsReceived = false
            });
            chatMessageInfo.Add(new ChatMessage()
            {
                Message = this.descriptions[2],
                Time = currentTime.AddMinutes(-1103),
                IsReceived = true
            });
            chatMessageInfo.Add(new ChatMessage()
            {
                Message = this.descriptions[3],
                Time = currentTime.AddMinutes(-1006),
                IsReceived = false
            });*/
        }

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
            if (!string.IsNullOrWhiteSpace(this.NewMessage))
            {
                /*this.ChatMessageInfo.Add(new ChatMessage
                {
                    Message = this.NewMessage,
                    Time = DateTime.Now
                });*/

                isFirstLoad = false;
                try
                {
                    SocioPress.Message.Instance.Insert(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, this.NewMessage, user_id, (bool success, string data) =>
                    {
                        if (success)
                        {
                            ChatList.Add(new ChatListItem("0", "", DateTime.Now, this.NewMessage, false));
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        }
                    });
                }
                catch (Exception)
                {
                    new Alert("Something went Wrong", "Please contact administrator.", "OK");
                }
                this.NewMessage = null;
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
                isFirstLoad = true;
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
                LoadMessage(user_id, ids.ToString());
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
