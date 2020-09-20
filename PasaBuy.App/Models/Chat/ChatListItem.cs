using PasaBuy.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Chat
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
	public class ChatListItem : BaseViewModel
	{

		private string id;

		private string message;

		private DateTime time;

		private string imagePath;
		public string ID
		{
			get
			{
				return this.id;
			}

			set
			{
				if (id != value)
				{
					this.id = value;
					this.NotifyPropertyChanged();
				}
			}
		}
		public string Message
		{
			get
			{
				return this.message;
			}

			set
			{
				if (message != value)
				{
					this.message = value;
					this.NotifyPropertyChanged();
				}
			}
		}
		public string ImagePath
		{
			get
			{
				return this.imagePath;
			}

			set
			{
				if (imagePath != value)
				{
					this.imagePath = value;
					this.NotifyPropertyChanged();
				}
			}
		}
		public DateTime Time
		{
			get
			{
				return this.time;
			}

			set
			{
				if (time != value)
				{
					this.time = value;
					this.NotifyPropertyChanged();
				}
			}
		}
		public Boolean IsReceived { get; set; }
		public ChatListItem(string id, string imagepath, DateTime time, string message, Boolean isreceived)
		{
			ID = id;
			ImagePath = imagepath;
			Time = time;
			Message = message;
			IsReceived = isreceived;
		}
	}
}
