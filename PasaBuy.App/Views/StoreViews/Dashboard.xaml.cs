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

            //this.BindingContext = new DashboardOrdersViewModel();
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
            if (!isClicked)
            {
                isClicked = true;
                if (e.TabItem.Title == "New Orders")
                {
                    //new Alert("New Orders", "New Orders", "New Orders");
                    DashboardOrdersViewModel.stages = "pending";
                    DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("pending");
                }
                if (e.TabItem.Title == "Pending")
                {
                    //new Alert("Pending", "Pending", "Pending");
                    DashboardOrdersViewModel.stages = "received";
                    DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("received");
                }
                if (e.TabItem.Title == "Declined")
                {
                    DashboardOrdersViewModel.stages = "cancelled";
                    DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("cancelled");
                    //new Alert("Declined", "Declined", "Declined");
                }
                if (e.TabItem.Title == "Completed")
                {
                    DashboardOrdersViewModel.stages = "shipping";
                    DashboardOrdersViewModel.orderList.Clear();
                    DashboardOrdersViewModel.LoadOrder("shipping");
                    //new Alert("Completed", "Completed", "Completed");
                }
                await Task.Delay(500);
                isClicked = false;
            }
        }
    }
}