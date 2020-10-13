using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PendingOrdersView : ContentView
    {
        public PendingOrdersView()
        {
            InitializeComponent();
            /*this.BindingContext = new DashboardOrdersViewModel();
            DashboardOrdersViewModel.orderList.Clear();
            DashboardOrdersViewModel.LoadOrder("received", "");*/
        }

        private void PendingOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as OrdersDataModel;
            TransactionDetailsView.id = item.ID;
            TransactionDetailsView.customer = item.Customer;
            TransactionDetailsView.orderid = item.OrderID;
            TransactionDetailsView.totalprice = item.TotalPrice;
            TransactionDetailsView.datecreated = item.Date_Time;
            TransactionDetailsView.method = item.Method;
            OrderDetailsViewModel.LoadOrder(item.Stage, item.ID);
            TransactionDetailsView.order_type = "Received";
            TransactionDetailsView.stage_type = "received";
            Navigation.PushModalAsync(new TransactionDetailsView());
        }
    }
}