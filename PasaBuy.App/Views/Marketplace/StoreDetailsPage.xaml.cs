using PasaBuy.App.Controllers.Notice;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.Marketplace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreDetailsPage : ContentPage
    {
 

        public StoreDetailsPage()
        {
            InitializeComponent();
            this.BindingContext = new PasaBuy.App.ViewModels.Marketplace.StoreDetailsViewModel();
            

        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            ViewModels.eCommerce.CartPageViewModel.refresh = 1;
            Navigation.PopModalAsync();
        }
       
    }
}