using PasaBuy.App.Local;
using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.StoreViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : ContentPage
    {
        public List<MenuItem> menuList { get; set; }
        protected Xamarin.Forms.Page CurrentDetail
        {
            get { return (Xamarin.Forms.Application.Current.MainPage as NavigationView).Detail; }
        }
        public MasterView()
        {
            InitializeComponent();
            Store_name.Text = PSACache.Instance.UserInfo.store_name;
            Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
            Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
            //UserName.Text = PSACache.Instance.UserInfo.dname;
            //Email.Text = PSACache.Instance.UserInfo.email;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            menuList = new List<MenuItem>();

            if (PSACache.Instance.UserInfo.stid != "0")
            {
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Dashboard"), Icon = "Idcard.png", TargetType = typeof(Dashboard) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Management"), Icon = "Idcard.png", TargetType = typeof(ManagementView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Products"), Icon = "Idcard.png", TargetType = typeof(ProductsView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Categories"), Icon = "Idcard.png", TargetType = typeof(CategoryView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Transactions"), Icon = "Idcard.png", TargetType = typeof(TransactionsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Messages"), Icon = "Idcard.png", TargetType = typeof(MessagesView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Vouchers"), Icon = "Idcard.png", TargetType = typeof(VouchersView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "Idcard.png", TargetType = typeof(DocumentsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Reports"), Icon = "Idcard.png", TargetType = typeof(ReportsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Settings"), Icon = "Idcard.png", TargetType = typeof(SettingsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Homepage"), Icon = "Idcard.png", TargetType = typeof(MainTabs) });
                navigationDrawerList.ItemsSource = menuList;
                navigationDrawerList.SelectedItem = menuList.FirstOrDefault();
            }


        }

        void Handle_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            if (!e.AddedItems.Any())
                return;

            var item = (MenuItem)e.AddedItems[0];
            //if (item.TargetType == typeof(LoginView))
            //{
            //    await Context.Logout();
            //}
            //else
            //{
            var page = (Xamarin.Forms.Page)Activator.CreateInstance(item.TargetType);
            Xamarin.Forms.NavigationPage navigatedPage = new Xamarin.Forms.NavigationPage(page);
            (Xamarin.Forms.Application.Current.MainPage as NavigationView).Detail = navigatedPage;
            if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                (Xamarin.Forms.Application.Current.MainPage as NavigationView).IsPresented = false;
            //}
        }

    }
}