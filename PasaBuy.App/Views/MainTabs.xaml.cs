using PasaBuy.App.ViewModels.Feeds;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.ViewModels.Menu;
using PasaBuy.App.Views.Feeds;
using PasaBuy.App.Views.Marketplace;
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
                    //StoreBrowserPage.LastIndex = 11;
                    StoreBrowserViewModel.itemCategories.Clear();
                    StoreBrowserViewModel.LoadCategory();
                }
                if (e.TabItem.Title == "GROCERY")
                {
                    GroceryBrowserPage.LastIndex = 11;
                    GroceryBrowserViewModel.grocerystorelist.Clear();
                    GroceryBrowserViewModel.LoadGrocery("");
                }
                if (e.TabItem.Title == "FOOD")
                {
                    FoodBrowserViewModel.RefreshData();
                }
                if (e.TabItem.Title == "PARTNER")
                {
                    //StoreBrowserPage.LastIndex = 11;
                    PartnerBrowserViewModel.itemCategories.Clear();
                    PartnerBrowserViewModel.LoadCategory();
                }
                /*if (e.TabItem.Title == "MESSAGE")
                {
                        MessagePage.LastIndex = 11;
                        MessagePage.isFirstID = false;
                        MessagePage.ids = 0;
                        RecentChatViewModel.chatItems.Clear();
                        RecentChatViewModel.LoadMesssage("");
                }*/
                await Task.Delay(500);
                isButton = false;
            }
        }
    }
}