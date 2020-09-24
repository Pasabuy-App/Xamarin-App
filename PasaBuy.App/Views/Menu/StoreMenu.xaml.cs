using PasaBuy.App.Resources.Texts;
using PasaBuy.App.Views.Backend;
using PasaBuy.App.Views.Menu.MenuItems;
using PasaBuy.App.Views.StoreViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreMenu : ContentPage
    {
        public List<MainMenuItem> menuList { get; set; }
        protected Xamarin.Forms.Page CurrentDetail
        {
            get { return (Xamarin.Forms.Application.Current.MainPage as StoreMain).Detail; }
        }
        public StoreMenu()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.Application.Current.Resources.TryGetValue("PrimaryColor", out var primeColor);
            //menuList = new List<MainMenuItem>();


            //menuList.Add(new MainMenuItem() { Title = TextsTranslateManager.Translate("Products"), Icon = "Idcard.png", TargetType = typeof(ProductsView) });
            //menuList.Add(new MainMenuItem() { Title = TextsTranslateManager.Translate("Documents"), Icon = "Idcard.png", TargetType = typeof(DocumentsView) });
            //menuList.Add(new MainMenuItem() { Title = TextsTranslateManager.Translate("Vouchers"), Icon = "Idcard.png", TargetType = typeof(VouchersView) });


            //navigationDrawerList.ItemsSource = menuList;
            //navigationDrawerList.SelectedItem = menuList.FirstOrDefault();
            var iconview = DashboardGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 0);
            var iconTar = iconview as Label;
            if (iconTar != null)
            {
                iconTar.TextColor = (Color)primeColor;
                viewModel.iconLabel = iconTar;
            }

            var textView = DashboardGrid.Children.FirstOrDefault(v => Grid.GetRow(v) == 0 && Grid.GetColumn(v) == 1);
            var textTar = textView as Label;
            if (textTar != null)
            {
                textTar.TextColor = (Color)primeColor;
                viewModel.textLabel = textTar;
            }
        }

        //async void Handle_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        //{
        //    if (!e.AddedItems.Any())
        //        return;

        //    var item = (MainMenuItem)e.AddedItems[0];
        //    //if (item.TargetType == typeof(LoginView))
        //    //{
        //    //    await Context.Logout();
        //    //}
        //    //else
        //    //{
        //    var page = (Xamarin.Forms.Page)Activator.CreateInstance(item.TargetType);
        //    Xamarin.Forms.NavigationPage navigatedPage = new Xamarin.Forms.NavigationPage(page);
        //    (Xamarin.Forms.Application.Current.MainPage as StoreMain).Detail = navigatedPage;
        //    if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
        //        (Xamarin.Forms.Application.Current.MainPage as StoreMain).IsPresented = false;
        //    //}
        //}
    }
}