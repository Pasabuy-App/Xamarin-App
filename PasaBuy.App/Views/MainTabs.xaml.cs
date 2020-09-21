using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.ViewModels.Chat;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.Chat;
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
        public int i = 0;
        public MainTabs()
        {
            InitializeComponent();
        }

        private async void TabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            //new Alert("TabViewItemTapped", e.TabItem.Title + " Item Tapped", "Ok");
            if (e.TabItem.Title == "HOME")
            {
                if (i == 0)
                {
                    i = 1;
                    ViewModels.Feeds.HomepageViewModel.homePostList.Clear();
                    ViewModels.Feeds.HomepageViewModel.LoadData();
                    ViewModels.Menu.MasterMenuViewModel.postbutton = string.Empty;
                    await Task.Delay(500);
                    i = 0;
                }
            }
            if (e.TabItem.Title == "STORE")
            {
                if (i == 0)
                {
                    i = 1;
                    StoreBrowserPage.LastIndex = 11;
                    StoreBrowserViewModel.storelist.Clear();
                    StoreBrowserViewModel.LoadStore("");
                    await Task.Delay(500);
                    i = 0;
                }
            }
            if (e.TabItem.Title == "GROCERY")
            {
                if (i == 0)
                {
                    i = 1;
                    GroceryBrowserPage.LastIndex = 11;
                    GroceryBrowserViewModel.grocerystorelist.Clear(); 
                    GroceryBrowserViewModel.LoadGrocery("");
                    await Task.Delay(500);
                    i = 0;
                }
            }
            if (e.TabItem.Title == "FOOD")
            {
                if (i == 0)
                {
                    i = 1;
                    FoodBrowserPage.LastIndex = 11;
                    FoodBrowserViewModel.foodstorelist.Clear();
                    FoodBrowserViewModel.LoadFood("");
                    await Task.Delay(500);
                    i = 0;
                }
            }
            if (e.TabItem.Title == "MESSAGE")
            {
                if (i == 0)
                {
                    i = 1;
                    MessagePage.LastIndex = 11;
                    MessagePage.isFirstID = false;
                    MessagePage.ids = 0;
                    RecentChatViewModel.chatItems.Clear();
                    RecentChatViewModel.LoadMesssage("");
                    await Task.Delay(500);
                    i = 0;
                }
            }
        }
    }
}