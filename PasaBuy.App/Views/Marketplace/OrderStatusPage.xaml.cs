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
            Navigation.PopModalAsync();
        }

        /*private void Mover_Clicked(object sender, EventArgs e)
        {
            new Controllers.Notice.Alert("Mover", "User message the mover.", "OK");
        }

        private void Store_Clicked(object sender, EventArgs e)
        {
            new Controllers.Notice.Alert("Store", "User message the store.", "OK");
        }*/
    }
}