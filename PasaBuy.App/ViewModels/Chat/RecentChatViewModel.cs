using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.Views;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Master;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.ViewModels.Chat
{
    /// <summary>
    /// View model for recent chat page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class RecentChatViewModel : BaseViewModel
    {
        #region Fields

        //private ObservableCollection<ChatDetail> chatItems;

        //private string profileImage = PSAConfig.sfRootUrl + "ProfileImage1.png";
        private string profileImage = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar) ;

        private Command itemSelectedCommand;

        public static ObservableCollection<ChatDetail> chatItems;

        public ObservableCollection<ChatDetail> ChatItems
        {
            get { return chatItems; }
            set { chatItems = value; this.NotifyPropertyChanged(); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentChatViewModel" /> class.
        /// </summary>
        public RecentChatViewModel()
        {
            chatItems = new ObservableCollection<ChatDetail>();
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
                SocioPress.Message.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, offset, (bool success, string data) =>
                {
                    if (success)
                    {
                        ChatDataList chat = JsonConvert.DeserializeObject<ChatDataList>(data);
                        for (int i = 0; i < chat.data.Length; i++)
                        {
                            string id = chat.data[i].ID;
                            string user_id = chat.data[i].user_id;
                            string name = chat.data[i].name;
                            string content = chat.data[i].content;
                            string date_created = chat.data[i].date_created == null ? "" : chat.data[i].date_created;
                            string date_seen = chat.data[i].date_seen == null ? "" : chat.data[i].date_seen;
                            string avatar = chat.data[i].avatar;
                            string sender_id = chat.data[i].sender_id;

                            string notitype = string.Empty;
                            if (sender_id == PSACache.Instance.UserInfo.wpid) { if (date_seen == "") { notitype = "Sent"; } else { notitype = "Received"; } }
                            else { if (date_seen == "") { notitype = "New"; } else { notitype = "Viewed"; } }

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

                            chatItems.Add(new ChatDetail()
                            {
                                ID = user_id,
                                ImagePath = PSAProc.GetUrl(avatar),
                                SenderName = name,
                                MessageType = "Text",
                                Message = content,
                                Time = showdate,
                                NotificationType = notitype
                            });
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
        private void SampleData()
        {
           /* this.ChatItems = new ObservableCollection<ChatDetail>
            {
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage2.png",
                    SenderName = "Alice Russell",
                    MessageType = "Text",
                    Message = "https://app.syncfusion",
                    Time = "15 min",
                    NotificationType = "New"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage3.png",
                    SenderName = "Danielle Schneider",
                    MessageType = "Audio",
                    Time = "23 min",
                    AvailableStatus = "Available",
                    NotificationType = "Viewed"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage4.png",
                    SenderName = "Jessica Park",
                    MessageType = "Text",
                    Message = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    Time = "1 hr",
                    NotificationType = "New"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage5.png",
                    SenderName = "Julia Grant",
                    MessageType = "Video",
                    Time = "3 hr",
                    AvailableStatus = "Available",
                    NotificationType = "Received"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage6.png",
                    SenderName = "kyle Greene",
                    MessageType = "Contact",
                    Message = "Jhone Deo Sync",
                    Time = "Yesterday",
                    NotificationType = "Viewed"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage7.png",
                    SenderName = "Danielle Booker",
                    MessageType = "Text",
                    Message = "Val Geisier is a writer who",
                    Time = "Jan 30",
                    AvailableStatus = "Available",
                    NotificationType = "Sent"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage8.png",
                    SenderName = "Jazmine Simmons",
                    MessageType = "Text",
                    Message = "Contrary to popular belief, Lorem Ipsum is not simply random text." +
                              "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.",
                    Time = "12/8/2018",
                    NotificationType = "Sent"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage9.png",
                    SenderName = "Ira Membrit",
                    MessageType = "Photo",
                    Time = "8/8/2018",
                    AvailableStatus = "Available",
                    NotificationType = "Viewed"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage10.png",
                    MessageType = "Text",
                    Message = "A customer who bought your",
                    SenderName = "Serina Willams",
                    Time = "10/6/2018",
                    NotificationType = "Sent"
                },
                 new ChatDetail
                 {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage11.png",
                    SenderName = "Alise Valasquez",
                    MessageType = "Text",
                    Message = "Syncfusion components help you deliver applications with great user experiences across iOS, Android, and Universal Windows Platform from a single code base.",
                    Time = "2/5/2018",
                    NotificationType = "New"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage12.png",
                    SenderName = "Allie Bellew",
                    MessageType = "Audio",
                    Time = "24/4/2018",
                    AvailableStatus = "Available",
                    NotificationType = "Viewed"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage13.png",
                    SenderName = "Navya Sharma",
                    MessageType = "Text",
                    Message = "https://www.syncfusion.com",
                    Time = "10/4/2018",
                    NotificationType = "New"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage14.png",
                    SenderName = "Carly Ling",
                    MessageType = "Video",
                    Time = "22/3/2018",
                    AvailableStatus = "Available",
                    NotificationType = "Received"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage15.png",
                    SenderName = "Diayana Sebastine",
                    MessageType = "Contact",
                    Message = "Kishore Nisanth",
                    Time = "15/3/2018",
                    NotificationType = "Viewed"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage16.png",
                    SenderName = "Marc Sherry",
                    MessageType = "Text",
                    Message = "Val Geisier is a writer who",
                    Time = "12/3/2018",
                    AvailableStatus = "Available",
                    NotificationType = "Sent"
                },
                new ChatDetail
                {
                    ImagePath = PSAConfig.sfRootUrl + "ProfileImage17.png",
                    SenderName = "Dona Merina",
                    MessageType = "Text",
                    Message = "Contrary to popular belief, Lorem Ipsum is not simply random text." +
                              "It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old.",
                    Time = "3/2/2018",
                    NotificationType = "Sent"
                },
            };*/
        }
        #endregion

        #region Public Properties

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
        /// Gets or sets the property that has been bound with a list view, which displays the profile items.
        /// </summary>
        /*public ObservableCollection<ChatDetail> ChatItems
        {
            get
            {
                return this.chatItems;
            }

            set
            {
                if (this.chatItems == value)
                {
                    return;
                }

                this.chatItems = value;
                this.NotifyPropertyChanged();
            }
        }*/

        #endregion

        #region Commands
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
            string user_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ID;
            string displayNames = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).SenderName;
            string profileImages = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as ChatDetail).ImagePath;
            //ChatMessageViewModel.LoadMessage(user_id, "");
            ChatMessageViewModel.ProfileNames = displayNames;
            ChatMessageViewModel.ProfileImages = profileImages;
            ChatMessageViewModel.user_id = user_id;
            ((MainTabs)App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new ChatMessagePage()));
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
