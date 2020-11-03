using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public bool isClicked = false;
        public Dashboard()
        {
            InitializeComponent();

            this.BindingContext = new DashboardOrdersViewModel();
            Title = "Dashboard";

        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                //Context.ScannedText = e.NewTextValue;
                //await Context.SearchProductWhenScan();
            }
        }

        private void NewOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {

        }

        private async void SfTabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (!IsRunning.IsRunning)
            {
                IsRunning.IsRunning = true;
                if (e.TabItem.Title == "Pending")
                {
                    DashboardOrdersViewModel.stages = "1";
                    DashboardOrdersViewModel.RefreshOrder("1");
                    await Task.Delay(300);
                    IsRunning.IsRunning = false;
                }
                if (e.TabItem.Title == "On-Going")
                {
                    DashboardOrdersViewModel.stages = "2";
                    DashboardOrdersViewModel.RefreshOrder("2");
                    await Task.Delay(300);
                    IsRunning.IsRunning = false;
                }
                if (e.TabItem.Title == "Completed")
                {
                    DashboardOrdersViewModel.stages = "3";
                    DashboardOrdersViewModel.RefreshOrder("3");
                    await Task.Delay(300);
                    IsRunning.IsRunning = false;
                }
                if (e.TabItem.Title == "Cancelled")
                {
                    DashboardOrdersViewModel.stages = "4";
                    DashboardOrdersViewModel.RefreshOrder("4");
                    await Task.Delay(300);
                    IsRunning.IsRunning = false;
                }
            }
        }
    }
}