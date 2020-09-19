using System;
using System.Collections.Generic;
using System.Text;

namespace PasaBuy.App.Models.Chat
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
	public class ChatListItem
	{
		public string ID { get; set; } = "";
		public string Message { get; set; } = "";
		public string ImagePath { get; set; } = "";
		public DateTime Time { get; set; }
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
