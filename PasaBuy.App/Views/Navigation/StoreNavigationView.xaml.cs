using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreNavigationView : MasterDetailPage
    {
        public List<MenuItem> menuList { get; set; }

        public StoreNavigationView()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PasaBuy.App.Views.StoreViews.Dashboard)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}