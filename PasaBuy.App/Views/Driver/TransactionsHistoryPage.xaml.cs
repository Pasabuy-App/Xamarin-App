
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsHistoryPage : ContentPage
    {
        public TransactionsHistoryPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.Driver.TransactionHistoryViewModel();
            pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        {
            pullToRefresh.IsRefreshing = true;
            await Task.Delay(500);
            ViewModels.Driver.TransactionHistoryViewModel.LastIndex = 11;
            ViewModels.Driver.TransactionHistoryViewModel._HistoryList.Clear();
            ViewModels.Driver.TransactionHistoryViewModel.LoadData("");
            pullToRefresh.IsRefreshing = false;
        }

        private void NewOrders_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            var item = e.ItemData as Models.Driver.TransactListData;
            if (ViewModels.Driver.TransactionHistoryViewModel._HistoryList.Last() == item && ViewModels.Driver.TransactionHistoryViewModel._HistoryList.Count() != 1)
            {
                if (ViewModels.Driver.TransactionHistoryViewModel._HistoryList.IndexOf(item) >= ViewModels.Driver.TransactionHistoryViewModel.LastIndex)
                {
                    ViewModels.Driver.TransactionHistoryViewModel.LastIndex += 6;
                    new Controllers.Notice.Alert("There is no ViewDetailsPage", "This is the id of Transaction : " + item.ID, "OK");
                    //ViewModels.Driver.TransactionHistoryViewModel._HistoryList.LoadData(item.Last_ID);
                }
            }
        }
    }
}