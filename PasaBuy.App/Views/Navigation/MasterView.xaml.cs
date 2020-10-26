using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.Driver;
using PasaBuy.App.Views.PopupModals;
using PasaBuy.App.Views.StoreViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : INotifyPropertyChanged
    {
        public static string MyType = string.Empty;
        public List<MenuItem> menuList { get; set; }
        protected Xamarin.Forms.Page CurrentDetail
        {
            get { return (Xamarin.Forms.Application.Current.MainPage as NavigationView).Detail; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public static ObservableCollection<Personnels> _logobanner;

        public ObservableCollection<Personnels> LogoBanner
        {
            get { return _logobanner; }
            set { _logobanner = value; OnPropertyChanged("LogoBanner"); }
        }
        public static ObservableCollection<Operations> _switchlist;

        public ObservableCollection<Operations> SwitchList
        {
            get { return _switchlist; }
            set { _switchlist = value; OnPropertyChanged("SwitchList"); }
        }
        public static bool _switch;
        public bool isTapped;
        public MasterView()
        {
            InitializeComponent();
            //UserName.Text = PSACache.Instance.UserInfo.dname;
            //Email.Text = PSACache.Instance.UserInfo.email;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            menuList = new List<MenuItem>();

            if (MyType == "store")
            {
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Dashboard"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/dashboard.png", TargetType = typeof(Dashboard) });
                Store_name.Text = PSACache.Instance.UserInfo.store_name;
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
                Email.Text = "";
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Point of Sales"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/pos.png", TargetType = typeof(StoreViews.POS.PointOfSales) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Management"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/management.png", TargetType = typeof(ManagementView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Categories"), Icon = "Idcard.png", TargetType = typeof(CategoryView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Transactions"), Icon = "Idcard.png", TargetType = typeof(TransactionsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Messages"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/messages.jpg", TargetType = typeof(MessagesView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Vouchers"), Icon = "Idcard.png", TargetType = typeof(VouchersView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "Idcard.png", TargetType = typeof(DocumentsView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Reports"), Icon = "Idcard.png", TargetType = typeof(ReportsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Settings"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/settings.png", TargetType = typeof(SettingsView) });


                _logobanner = new ObservableCollection<Personnels>();
                _logobanner.CollectionChanged += LogoBannerChanges;
                _switchlist = new ObservableCollection<Operations>();
                _switchlist.CollectionChanged += SwitchChanges;
                PopupGoOnline._switch = "false";
                isTapped = false;
                _switch = false;
                ValidateStatus();
            }
            if (MyType == "mover")
            {
                //Views.Driver.Navigation.StoreAddress = string.Empty;
                //Views.Driver.Navigation.UserAddress = string.Empty;
                Store_name.Text = PSACache.Instance.UserInfo.dname;
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.banner);
                Email.Text = PSACache.Instance.UserInfo.email;
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Dashboard"), Icon = "Idcard.png", TargetType = typeof(Views.Driver.DashboardPage) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Transactions"), Icon = "Idcard.png", TargetType = typeof(TransactionsHistoryPage) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Messages"), Icon = "Idcard.png", TargetType = typeof(MessagesView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "Idcard.png", TargetType = typeof(DocumentsView) });
            }

            menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Homepage"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/home.jpg", TargetType = typeof(MainTabs) });
            navigationDrawerList.ItemsSource = menuList;
            navigationDrawerList.SelectedItem = menuList.FirstOrDefault();


        }
        private void SwitchChanges(object sender, EventArgs e)
        {
            if (_switch)
            {
                isTapped = true;
                isActiveSwitch.IsOn = true;
                Status.Text = "Online";
                isTapped = false;
                //_switch = false;
            }
            else
            {
                isTapped = true;
                isActiveSwitch.IsOn = false;
                Status.Text = "Offline";
                isTapped = false;
                //_switch = true;
            }
        }
        public void ValidateStatus()
        {
            if (PSACache.Instance.UserInfo.store_status)
            {
                isTapped = true;
                isActiveSwitch.IsOn = true;
                Status.Text = "Online";
                isTapped = false;
            }
            else
            {
                isTapped = true;
                isActiveSwitch.IsOn = false;
                Status.Text = "Offline";
                isTapped = false;
            }
        }
        private async void LogoBannerChanges(object sender, EventArgs e)
        {
            await Task.Delay(100);
            if (MyType == "mover")
            {
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.banner);
            }
            if (MyType == "store")
            {
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
            }
        }
        public static void Insertimage(string url)
        {
            _logobanner.Add(new Personnels()
            {
                Avatar = url
            });
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

        private async void isActive(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                if (MyType == "store")
                {
                    PopupGoOnline._switch = isActiveSwitch.IsOn.ToString();
                    await PopupNavigation.Instance.PushAsync(new PopupGoOnline());
                }
                if (MyType == "mover")
                {
                    PopupGoOnline._switch = isActiveSwitch.IsOn.ToString();
                    await PopupNavigation.Instance.PushAsync(new PopupGoOnline());
                }

                isTapped = false;
                //Console.WriteLine("isActiveSwitch.IsOn.ToString(): " + isActiveSwitch.IsOn.ToString());
                //new Controllers.Notice.Alert("Switch", isActiveSwitch.IsOn.ToString(), "Ok");
            }
        }
    }
}