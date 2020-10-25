using PasaBuy.App.Models.MobilePOS;
using PasaBuy.App.ViewModels.MobilePOS;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.StoreViews.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeclinedOrdersView : ContentView
    {
        public bool isTapped;
        public DeclinedOrdersView()
        {
            InitializeComponent();
            isTapped = false;
            /*this.BindingContext = new DashboardOrdersViewModel();
            DashboardOrdersViewModel.orderList.Clear();
            DashboardOrdersViewModel.LoadOrder("cancelled", "")*/
            ;
        }

        private async void DeclinedOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
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
                TransactionDetailsView.order_type = "Declined";
                OrderDetailsViewModel.LoadOrder(item.Stage, Local.PSACache.Instance.UserInfo.stid, item.ID);
                await Navigation.PushModalAsync(new TransactionDetailsView());
                await System.Threading.Tasks.Task.Delay(500);
                isTapped = false;
            }
        }
    }
}