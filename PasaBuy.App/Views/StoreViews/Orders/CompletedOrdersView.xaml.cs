using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedOrdersView : ContentView
    {
        public CompletedOrdersView()
        {
            InitializeComponent();
            /*this.BindingContext = new DashboardOrdersViewModel();
            DashboardOrdersViewModel.orderList.Clear();
            DashboardOrdersViewModel.LoadOrder("shipping", "");*/
        }

        private void CompletedOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as OrdersDataModel;
            TransactionDetailsView.id = item.ID;
            TransactionDetailsView.customer = item.Customer;
            TransactionDetailsView.orderid = item.OrderID;
            TransactionDetailsView.totalprice = item.TotalPrice;
            TransactionDetailsView.datecreated = item.Date_Time;
            TransactionDetailsView.method = item.Method;
            TransactionDetailsView.order_type = "Completed";
            OrderDetailsViewModel.LoadOrder(item.Stage, item.ID);
            Navigation.PushModalAsync(new TransactionDetailsView());
        }
    }
}