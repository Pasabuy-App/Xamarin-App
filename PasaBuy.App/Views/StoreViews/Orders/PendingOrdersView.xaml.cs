using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingOrdersView : ContentView
    {
        public bool isTapped;
        public PendingOrdersView()
        {
            InitializeComponent();
            isTapped = false;
            /*this.BindingContext = new DashboardOrdersViewModel();
            DashboardOrdersViewModel.orderList.Clear();
            DashboardOrdersViewModel.LoadOrder("received", "");*/
        }

        private async void PendingOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (!isTapped)
            {
                isTapped = true;
                var item = e.ItemData as OrdersDataModel;
                TransactionDetailsView.id = item.ID;
                TransactionDetailsView.avatar = item.Avatar;
                TransactionDetailsView.user_id = item.User_ID;
                TransactionDetailsView.customer = item.Customer;
                TransactionDetailsView.orderid = item.OrderID;
                TransactionDetailsView.totalprice = item.TotalPrice;
                TransactionDetailsView.datecreated = item.Date_Time;
                TransactionDetailsView.method = item.Method;
                OrderDetailsViewModel.LoadOrder(item.Stage, Local.PSACache.Instance.UserInfo.stid, item.ID);
                TransactionDetailsView.order_type = "Received";
                TransactionDetailsView.stage_type = "received";
                await Navigation.PushModalAsync(new TransactionDetailsView());
                await System.Threading.Tasks.Task.Delay(500);
                isTapped = false;
            }
        }
    }
}