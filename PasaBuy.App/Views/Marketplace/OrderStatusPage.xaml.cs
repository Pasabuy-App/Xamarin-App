using PasaBuy.App.ViewModels.Marketplace;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderStatusPage : ContentPage
    {
        public OrderStatusPage()
        {
            InitializeComponent();
            this.BindingContext = new OrderStatusViewModel();

        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            OrderStatusViewModel.isTimer = false;
            OrderStatusViewModel.myTimer = false;
            Navigation.PopModalAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            OrderStatusViewModel.isTimer = false;
            OrderStatusViewModel.myTimer = false;
            return base.OnBackButtonPressed();
        }

    }
}