using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.Models.Driver
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class ChatList : ObservableCollection<ChatListItem>
    {
        public ChatList(List<ChatListDetails> items) : base()
        {
            foreach (var item in items)
                Insert(0, new ChatListItem(item.id, item.imagepath, item.time, item.message, item.isreceived));
        }
    }
}
