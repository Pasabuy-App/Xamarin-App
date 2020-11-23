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

        public static ObservableCollection<Models.POSFeature.PersonnelModel> _logobanner;

        public ObservableCollection<Models.POSFeature.PersonnelModel> LogoBanner
        {
            get { return _logobanner; }
            set { _logobanner = value; OnPropertyChanged("LogoBanner"); }
        }
        public static ObservableCollection<Models.POSFeature.OperationModel> _switchlist;

        public ObservableCollection<Models.POSFeature.OperationModel> SwitchList
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
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Dashboard"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/dashboard-prim.png", TargetType = typeof(Dashboard) });
                Store_name.Text = PSACache.Instance.UserInfo.store_name;
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
                Email.Text = PSACache.Instance.UserInfo.store_info;

                Switch.IsVisible = false;
                foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
                {
                    if (per.action == "online_offline_store")
                    {
                        Switch.IsVisible = true;
                        break;
                    }
                }

                foreach (var per in ViewModels.MobilePOS.MyStoreListViewModel.permissions)
                {
                    if (per.action == "check_product")
                    {
                        menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Point of Sales"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/pos-prim.png", TargetType = typeof(StoreViews.POS.PointOfSales) });
                        break;
                    }
                }

                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Management"), Icon = "https://dev.pasabuy.appmanagement-prim.png", TargetType = typeof(ManagementView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Categories"), Icon = "Idcard.png", TargetType = typeof(CategoryView) });/wp-content/uploads/2020/11/
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Transactions"), Icon = "Idcard.png", TargetType = typeof(TransactionsView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Messages"), Icon = "https://pasabuy.app/wp-content/uploads/2020/10/messages.jpg", TargetType = typeof(MessagesView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Vouchers"), Icon = "Idcard.png", TargetType = typeof(VouchersView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "Idcard.png", TargetType = typeof(DocumentsView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Reports"), Icon = "Idcard.png", TargetType = typeof(ReportsView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Settings"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/settings-prim.png", TargetType = typeof(SettingsView) });


                _logobanner = new ObservableCollection<Models.POSFeature.PersonnelModel>();
                _logobanner.CollectionChanged += LogoBannerChanges;
            }
            if (MyType == "mover")
            {
                Switch.IsVisible = true;
                //Views.Driver.Navigation.StoreAddress = string.Empty;
                //Views.Driver.Navigation.UserAddress = string.Empty;
                Store_name.Text = PSACache.Instance.UserInfo.dname;
                Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.avatar);
                Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.banner);
                Email.Text = PSACache.Instance.UserInfo.email;
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Dashboard"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/dashboard-prim.png", TargetType = typeof(Views.Driver.DashboardPage) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Transactions"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/transactions-prim.png", TargetType = typeof(TransactionsHistoryPage) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("My Wallet"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/wallet-prim.png", TargetType = typeof(DriverWalletView) });
                //menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Messages"), Icon = "Idcard.png", TargetType = typeof(MessagesView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/documents-prim.png", TargetType = typeof(DriverDocumentView) });
                menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Schedules"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/schedules-prim.png", TargetType = typeof(DriverScheduleView) });
                
            }
            _switchlist = new ObservableCollection<Models.POSFeature.OperationModel>();
            _switchlist.CollectionChanged += SwitchChanges;
            PopupGoOnline._switch = "false";
            isTapped = false;
            _switch = false;

            menuList.Add(new MenuItem() { Title = TextsTranslateManager.Translate("Homepage"), Icon = "https://dev.pasabuy.app/wp-content/uploads/2020/11/home.png", TargetType = typeof(MainTabs) });
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
            }
            else
            {
                isTapped = true;
                isActiveSwitch.IsOn = false;
                Status.Text = "Offline";
                isTapped = false;
            }
        }

        public void ValidateStatus() // no need, save status of store or mover in cache
        {
            if (MyType == "store")
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
            if (MyType == "mover")
            {
                if (PSACache.Instance.UserInfo.mover_status)
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
        }

        private void LogoBannerChanges(object sender, EventArgs e)
        {
            Logo.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_logo);
            Banner.Source = PSAProc.GetUrl(PSACache.Instance.UserInfo.store_banner);
        }

        public static void Insertimage(string url)
        {
            _logobanner.Add(new Models.POSFeature.PersonnelModel()
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
            if (item.TargetType == typeof(MainTabs))
            {
                DashboardPage.locs = false;
            }
            DashboardPage._starttimer = false;
            ViewModels.MobilePOS.DashboardOrdersViewModel._starttimer = false;
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
                    //await PopupNavigation.Instance.PushAsync(new PopupSelectSchedule());
                    await PopupNavigation.Instance.PushAsync(new PopupGoOnline());
                }

                isTapped = false;
            }
        }
    }
}