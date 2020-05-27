using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            //TODO: SHOW HOME FEED.
            //Detail = new NavigationPage(new MainPageDetail());
            //IsPresented = false;
        }

        public void HideSidebar()
        {
            IsPresented = false;
        }
    }
}