using Syncfusion.DataSource;
using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Chat
{
    /// <summary>
    /// Page to show chat message list
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatMessagePage
    {
        //SfListView listView = new SfListView();
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessagePage" /> class.
        /// </summary>
        public ChatMessagePage()
        {
            InitializeComponent();
            /*ListView.DataSource.GroupDescriptors.Add(new GroupDescriptor
            {
                PropertyName = "Time",
                KeySelector = obj =>
                {
                    var item = obj as Models.Chat.ChatListItem;
                    return item.Time.Date;
                }
            });*/
        }
        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            //RecentChatViewModel.chatItems.Clear();
            //RecentChatViewModel.LoadMesssage("");
            //ChatMessageViewModel.refresh = 1;
            Navigation.PopModalAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            //RecentChatViewModel.chatItems.Clear();
            //RecentChatViewModel.LoadMesssage("");
            //ChatMessageViewModel.refresh = 1;
            return base.OnBackButtonPressed();
        }

    }
}