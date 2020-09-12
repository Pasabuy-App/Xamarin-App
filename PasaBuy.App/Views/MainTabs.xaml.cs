using PasaBuy.App.Controllers.Notice;
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
                PasaBuy.App.ViewModels.Feeds.HomepageViewModel.homePostList.Clear();
                PasaBuy.App.ViewModels.Feeds.HomepageViewModel.LoadData();
                PasaBuy.App.ViewModels.Menu.MasterMenuViewModel.postbutton = string.Empty;

            }
        }
    }
}