using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Library;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.PopupModals;
using Rg.Plugins.Popup.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabs : ContentPage
    {
        public bool isButton = false;
        public int i = 0;
        public static string verify;
        public MainTabs()
        {
            InitializeComponent();
            if (Local.PSACache.Instance.UserInfo.verify == "UNVERIFIED")
            {
                UserVerify();
            }
            isButton = false;

            ShowPasabuyStores = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushModalAsync(page);
            });

            BindingContext = this;

            

        }


        public ICommand ShowPasabuyStores { private set; get; }

        public static void UserVerify()
        {
            try
            {
                Http.DataVice.Users.Instance.VerifyUser((bool success, string data) =>
                {
                    if (success)
                    {
                        verify = "VERIFIED";
                    }
                    else
                    {
                        PopupNavigation.Instance.PushAsync(new PopupStartup());
                        verify = "UNVERIFIED";
                    }
                    Local.PSACache.Instance.UserInfo.verify = verify;
                    Local.PSACache.Instance.SaveUserData();
                });
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1URS-VU1MT.", "OK");
            }
        }
        private async void TabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (!isButton)
            {
                isButton = true;
                if (e.TabItem.Title == "HOME")
                {
                    /*HomePage.LastIndex = 12;
                    HomePage.isFirstLoad = false;
                    HomepageViewModel.homePostList.Clear();
                    HomepageViewModel.LoadData("");
                    MasterMenuViewModel.postbutton = string.Empty;*/
                }
                if (e.TabItem.Title == "STORE")
                {
                    //StoreBrowserPage.LastIndex = 11;
                    //StoreBrowserViewModel.LoadCategory();
                    //StoreBrowserViewModel.itemCategories.Clear();
                }
                if (e.TabItem.Title == "GROCERY")
                {
                    /*GroceryBrowserPage.LastIndex = 11;
                    GroceryBrowserViewModel.grocerystorelist.Clear();
                    GroceryBrowserViewModel.LoadGrocery("");*/
                }
                if (e.TabItem.Title == "FOOD")
                {
                    //FoodBrowserViewModel.RefreshData();
                }
                if (e.TabItem.Title == "PARTNER")
                {
                    //StoreBrowserPage.LastIndex = 11;
                    //PartnerBrowserViewModel.LoadCategory();
                    /*PartnerBrowserViewModel.storeList.Clear();
                    PartnerBrowserViewModel.LoadPartner("");
                    PartnerBrowserViewModel.partnerStoresRotator.Clear();
                    PartnerBrowserViewModel.LoadRotator();*/
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