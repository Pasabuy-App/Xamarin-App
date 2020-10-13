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

        }
        public void BackButtonClicked(object sender, EventArgs e)
        {
            ViewModels.eCommerce.CartPageViewModel.refresh = 1;
            Navigation.PopModalAsync();
        }

        private void AddToCart(object sender, EventArgs e)
        {
            new Alert("Ok", "ok", "ok");
        }


        /*private void CategoryTapped(object sender, EventArgs e)
        {*/
        //var item = e.ItemData as StoreDetails;
        /* var btn = (TapGestureRecognizer)sender;
         var id = btn.ClassId;
         new Alert("sample", "data: "+ id, "ok");*/

        //}
    }
}