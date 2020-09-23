using PasaBuy.App.Views.MobilePOS.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace PasaBuy.App.Views.MobilePOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationView : MasterDetailPage
    {
        public List<MainMenuItem> menuList { get; set; }

        public NavigationView()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(MainView)));

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_context.CheckLogin();
        }
    }
}