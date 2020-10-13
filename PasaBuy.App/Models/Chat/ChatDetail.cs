using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace PasaBuy.App.Models.Chat
{
    /// <summary>
    /// Model for recent chat
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Public Properties

        private string id = string.Empty;
        private string imagepath = string.Empty;
        private string store_id = string.Empty;
        private string types = string.Empty;
        private string sendername = string.Empty;
        private string time = string.Empty;
        private string message = string.Empty;
        private string messagetype = string.Empty;
        private string notificationtype = string.Empty;
        private string availablestatus = string.Empty;
        /// <summary>
        /// Gets or sets the profile id.
        /// </summary>
        public string Store_id
        {
            get { return store_id; }
            set
            {
                store_id = value;
                OnPropertyChanged("Store_id");
            }
        }
        public string Types
        {
            get { return types; }
            set
            {
                types = value;
                OnPropertyChanged("Types");
            }
        }
        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or sets the profile image path.
        /// </summary>
        public string ImagePath
        {
            get { return imagepath; }
            set
            {
                imagepath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        /// <summary>
        /// Gets or sets the profile name.
        /// </summary>
        public string SenderName
        {
            get { return sendername; }
            set
            {
                sendername = value;
                OnPropertyChanged("SenderName");
            }
        }

        /// <summary>
        /// Gets or sets the message sent/received time.
        /// </summary>
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Gets or sets the MessageType.
        /// </summary>
        public string MessageType
        {
            get { return messagetype; }
            set
            {
                messagetype = value;
                OnPropertyChanged("MessageType");
            }
        }

        /// <summary>
        /// Gets or sets the message sent/received notification type.
        /// </summary>
        public string NotificationType
        {
            get { return notificationtype; }
            set
            {
                notificationtype = value;
                OnPropertyChanged("NotificationType");
            }
        }

        /// <summary>
        /// Gets or sets the recipient's available status.
        /// </summary>
        public string AvailableStatus
        {
            get { return availablestatus; }
            set
            {
                availablestatus = value;
                OnPropertyChanged("AvailableStatus");
            }
        }

        #endregion
        public ChatDetail(string id, string imagepath, string sendername, string time, string message, string messagetype, string notificationtype, string availablestatus, string store_id, string types)
        {
            this.id = id;
            this.imagepath = imagepath;
            this.sendername = sendername;
            this.time = time;
            this.message = message;
            this.messagetype = messagetype;
            this.notificationtype = notificationtype;
            this.availablestatus = availablestatus;
            this.store_id = store_id;
            this.types = types;
        }
    }
}
