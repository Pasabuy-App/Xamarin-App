using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace PasaBuy.App.Views.Navigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationView : MasterDetailPage
    {
        public NavigationView()
        {
            InitializeComponent();
            if (MasterView.MyType == "store")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PasaBuy.App.Views.StoreViews.Dashboard)));
            }
            if (MasterView.MyType == "mover")
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PasaBuy.App.Views.Driver.DashboardPage)));
            }


        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_context.CheckLogin();
        }
    }
}