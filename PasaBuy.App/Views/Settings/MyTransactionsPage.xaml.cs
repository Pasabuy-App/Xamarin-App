using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTransactionsPage : ContentPage
    {
        public MyTransactionsPage()
        {
            InitializeComponent();
        }

        public void BackButtonClicked(object sender, EventArgs e)
        {
            ViewModels.Marketplace.OrderStatusViewModel.isTimer = false;
            Navigation.PopModalAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            ViewModels.Marketplace.OrderStatusViewModel.isTimer = false;
            return base.OnBackButtonPressed();
        }

        private void transactionList_ItemAppearing(object sender, Syncfusion.ListView.XForms.ItemAppearingEventArgs e)
        {
            // 12 - 7
        }
    }
}