using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Models.Chat;
using PasaBuy.App.ViewModels.Chat;
using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
                    var item = obj as ChatListItem;
                    return item.Time.Date;
                }
            });*/
        }
        /// <summary>
        /// Invokes when back button is clicked.
        /// </summary>
        public void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}