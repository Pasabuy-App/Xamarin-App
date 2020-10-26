using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public bool isClicked = false;
        public MainView()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
        }

        private async void NewOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isClicked)
            {
                isClicked = true;
                var item = e.ItemData as OrdersDataModel;
                TransactionDetailsView.id = item.ID;
                TransactionDetailsView.customer = item.Customer;
                TransactionDetailsView.orderid = item.OrderID;
                TransactionDetailsView.totalprice = item.TotalPrice;
                TransactionDetailsView.datecreated = item.Date_Time;
                TransactionDetailsView.method = item.Method;
                TransactionDetailsView.order_type = "Pending";
                TransactionDetailsView.stage_type = "pending";
                OrderDetailsViewModel.LoadOrder(Local.PSACache.Instance.UserInfo.stid, "", item.ID);
                await Navigation.PushModalAsync(new TransactionDetailsView());
                await Task.Delay(500);
                isClicked = false;
            }
        }

        private async void SfTabView_TabItemTapped(object sender, Syncfusion.XForms.TabView.TabItemTappedEventArgs e)
        {
            if (!isClicked)
            {
                isClicked = true;
                if (e.TabItem.Title == "New Orders")
                {
                    //new Alert("New Orders", "New Orders", "New Orders");
                    //DashboardOrdersViewModel.orderList.Clear();
                    MainViewModel.LoadOrder("pending", "");
                }
                if (e.TabItem.Title == "Pending")
                {
                    //new Alert("Pending", "Pending", "Pending");
                    //DashboardOrdersViewModel.orderList.Clear();
                    MainViewModel.LoadOrder("received", "");
                }
                if (e.TabItem.Title == "Declined")
                {
                    //DashboardOrdersViewModel.orderList.Clear();
                    MainViewModel.LoadOrder("cancelled", "");
                    //new Alert("Declined", "Declined", "Declined");
                }
                if (e.TabItem.Title == "Completed")
                {
                    //DashboardOrdersViewModel.orderList.Clear();
                    MainViewModel.LoadOrder("shipping", "");
                    //new Alert("Completed", "Completed", "Completed");
                }
                await Task.Delay(200);
                isClicked = false;
            }
        }

    }
}