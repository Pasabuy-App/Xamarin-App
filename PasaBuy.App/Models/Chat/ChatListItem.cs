using System;
using System.ComponentModel;

namespace PasaBuy.App.Models.Chat
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class ChatListItem : INotifyPropertyChanged
    {
        #region Fields

        private string id;

        private string message;

        private DateTime time;

        private string imagePath;

        #endregion

        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public string ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Gets or sets the message sent/received time.
        /// </summary>
        public DateTime Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                this.OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }

            set
            {
                this.imagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the message is received or sent.
        /// </summary>
        public bool IsReceived { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The PropertyChanged event occurs when property value is changed.
        /// </summary>
        /// <param name="property">property name</param>
        protected void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public ChatListItem(string id, string imagepath, DateTime time, string message, Boolean isreceived)
        {
            ID = id;
            ImagePath = imagepath;
            Time = time;
            Message = message;
            IsReceived = isreceived;
        }

        #endregion
    }
}