using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeclinedOrdersView : ContentView
    {
        public DeclinedOrdersView()
        {
            InitializeComponent();
            /*this.BindingContext = new DashboardOrdersViewModel();
            DashboardOrdersViewModel.orderList.Clear();
            DashboardOrdersViewModel.LoadOrder("cancelled", "")*/
            ;
        }

        private void DeclinedOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as OrdersDataModel;
            TransactionDetailsView.id = item.ID;
            TransactionDetailsView.customer = item.Customer;
            TransactionDetailsView.orderid = item.OrderID;
            TransactionDetailsView.totalprice = item.TotalPrice;
            TransactionDetailsView.datecreated = item.Date_Time;
            TransactionDetailsView.method = item.Method;
            TransactionDetailsView.order_type = "Declined";
            OrderDetailsViewModel.LoadOrder(item.Stage, item.ID);
            Navigation.PushModalAsync(new TransactionDetailsView());
        }
    }
}