using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        private void DeclinedOrders_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new TransactionDetailsView());
        }
    }
}