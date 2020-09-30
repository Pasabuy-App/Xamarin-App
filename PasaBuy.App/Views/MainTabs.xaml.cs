using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Chat;
using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.ViewModels.Menu;
using PasaBuy.App.Views.Chat;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Marketplace;
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
        public bool isButton = false;
        public int i = 0;
        public MainTabs()
        {
            InitializeComponent();
            isButton = false;
        }

        private async void TabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (!isButton)
            {
                isButton = true;
                if (e.TabItem.Title == "HOME")
                {
                        HomePage.LastIndex = 11;
                        HomepageViewModel.homePostList.Clear();
                        HomepageViewModel.LoadData("");
                        MasterMenuViewModel.postbutton = string.Empty;
                }
                if (e.TabItem.Title == "STORE")
                {
                        StoreBrowserPage.LastIndex = 11;
                        StoreBrowserViewModel.storelist.Clear();
                        StoreBrowserViewModel.LoadStore("");
                }
                if (e.TabItem.Title == "GROCERY")
                {
                        GroceryBrowserPage.LastIndex = 11;
                        GroceryBrowserViewModel.grocerystorelist.Clear();
                        GroceryBrowserViewModel.LoadGrocery("");
                }
                if (e.TabItem.Title == "FOOD")
                {
                        FoodBrowserPage.LastIndex = 11;
                        FoodBrowserViewModel.foodstorelist.Clear();
                        FoodBrowserViewModel.LoadFood("");
                }
                if (e.TabItem.Title == "MESSAGE")
                {
                        MessagePage.LastIndex = 11;
                        MessagePage.isFirstID = false;
                        MessagePage.ids = 0;
                        RecentChatViewModel.chatItems.Clear();
                        RecentChatViewModel.LoadMesssage("");
                }
                await Task.Delay(500);
                isButton = false;
            }
        }
    }
}