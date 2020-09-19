using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Chat;
using PasaBuy.App.Views.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabs : ContentPage
    {
        public MainTabs()
        {
            InitializeComponent();
        }

        private void TabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            //new Alert("TabViewItemTapped", e.TabItem.Title + " Item Tapped", "Ok");
            if (e.TabItem.Title == "HOME")
            {
                ViewModels.Feeds.HomepageViewModel.homePostList.Clear();
                ViewModels.Feeds.HomepageViewModel.LoadData();
                ViewModels.Menu.MasterMenuViewModel.postbutton = string.Empty;
            }
            if (e.TabItem.Title == "MESSAGE")
            {
                MessagePage.LastIndex = 11;
                MessagePage.isFirstID = false;
                MessagePage.ids = 0;
                RecentChatViewModel.chatItems.Clear();
                RecentChatViewModel.LoadMesssage("");
            }
        }
    }
}